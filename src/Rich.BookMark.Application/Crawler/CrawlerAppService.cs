using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rich.BookMark.Crawler.BookMarkDto;
using Rich.BookMark.Crawlers;
using Volo.Abp.Uow;

namespace Rich.BookMark.Crawler;

/// <summary>
/// https://www.Jizhihezi.com/ 爬虫
/// </summary>
public partial class CrawlerAppService : BookMarkAppService, ICrawlerAppService
{
    // private readonly ICrawlerMenuRepository _crawlerMenuRepository;
    private readonly ICrawlerDetailRepository _crawlerDetailRepository;

    private readonly ICrawlerMenuManager _crawlerMenuManager;
    // private readonly ICrawlerDetailCache _crawlerDetailCache;


    public CrawlerAppService(
        ICrawlerDetailRepository crawlerDetailRepository,
        ICrawlerMenuManager crawlerMenuManager)
    {
        _crawlerDetailRepository = crawlerDetailRepository;
        _crawlerMenuManager = crawlerMenuManager;
    }

    /// <summary>
    /// 采集数据入库
    /// </summary>
    /// <param name="data"></param>
    /// <param name="spiderSource"></param>
    [UnitOfWork]
    public async Task SeedData(ResultData data, SpiderSourceEnum spiderSource)
    {
        var menus = new List<CrawlerMenu>();
        foreach (var menuDto in data.Menus)
        {
            var list_childMenuDto = GetChildMenus(menuDto);
            list_childMenuDto.Add(menuDto);

            menus.AddRange(ObjectMapper.Map<ICollection<MenuDto>, ICollection<CrawlerMenu>>(list_childMenuDto));
        }

        menus.ForEach(p =>
        {
            p.SpiderSource = spiderSource;
            p.Sort = int.Parse(p.MenuId);
        });


        var details = ObjectMapper.Map<IEnumerable<BookMarkDetailDto>, IEnumerable<CrawlerDetail>>(data.Details);
        foreach (var detail in details)
        {
            detail.SpiderSource = spiderSource;
        }
        
        await _crawlerMenuManager.DeleteMenuByDataSource(spiderSource);
        await _crawlerDetailRepository.DeleteDetailByDataSource(spiderSource);

        await _crawlerMenuManager.InsertManyAsync(menus);
        await _crawlerDetailRepository.InsertManyAsync(details);
    }


    /// <summary>
    /// 从数据库获取集知盒子菜单
    /// </summary>
    /// <returns></returns>
    public async Task<List<MenuDto>> GetJiZhiHeZiMenu()
    {
        //var menus = await _crawlerMenuRepository.GetMenus(SpiderSourceEnum.jizhihezi);

        var menus = await _crawlerMenuManager.GetMenusAsync(SpiderSourceEnum.jizhihezi);
        var rootMenus = menus.Where(x => x.ParentId == "0").ToList();

        var list_rootMenu = rootMenus.Select(rootMenu =>
        {
            var rootMenuDto = ObjectMapper.Map<CrawlerMenu, MenuDto>(rootMenu);
            AddMenu(rootMenuDto, menus);
            return rootMenuDto;
        }).ToList();

        return list_rootMenu;
    }

    /// <summary>
    /// 从数据库获取集知盒子详情数据
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<List<BookMarkDetailDto>> GetJiZhiHeZiDetailes(string menuId)
    {
        var result = await _crawlerDetailRepository.GetDetailsByMenuId(menuId);
        return ObjectMapper.Map<List<CrawlerDetail>, List<BookMarkDetailDto>>(result);
    }

    public async Task<ResultData> GetJiZhiHeZiMenuAndDetails()
    {
        var menus = await GetJiZhiHeZiMenu();
        var details = await _crawlerDetailRepository.GetListAsync();
        //var details = await _crawlerDetailCache.GetDetailsAsync(SpiderSourceEnum.jizhihezi);

        return new ResultData()
        {
            Menus = menus,
            Details = ObjectMapper.Map<List<CrawlerDetail>, List<BookMarkDetailDto>>(details)
        };
    }


    private void AddMenu(MenuDto parentMenu, List<CrawlerMenu> list_menu)
    {
        var newMenus = list_menu.Where(x => x.ParentId == parentMenu.MenuId);

        foreach (var menu in newMenus)
        {
            var menuDto = ObjectMapper.Map<CrawlerMenu, MenuDto>(menu);
            parentMenu.Menus.Add(menuDto);
            AddMenu(menuDto, list_menu);
        }
    }


    /// <summary>
    /// 递归获取Menu菜单
    /// </summary>
    /// <param name="menu"></param>
    /// <returns></returns>
    private ICollection<MenuDto> GetChildMenus(MenuDto menu)
    {
        var childMenus = new List<MenuDto>();
        foreach (var childMenu in menu.Menus)
        {
            childMenus.Add(childMenu);
            childMenus.AddRange(GetChildMenus(childMenu));
        }

        return childMenus;
    }
}