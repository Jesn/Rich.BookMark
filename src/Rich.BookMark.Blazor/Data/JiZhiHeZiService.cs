using System.Collections.Generic;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;
using Rich.BookMark.Crawler.BookMarkDto;
using Volo.Abp.DependencyInjection;


namespace Rich.BookMark.Blazor.Data;

public class JiZhiHeZiService : ITransientDependency
{
    private readonly ICrawlerAppService _crawlerAppService;

    public JiZhiHeZiService(ICrawlerAppService crawlerAppService)
    {
        _crawlerAppService = crawlerAppService;
    }

    public async Task<List<MenuDto>> GetMenus()
    {
        return await _crawlerAppService.GetJiZhiHeZiMenu();
    }

    public async Task<ResultData> GetJiZhiHeZiMenuAndDetails()
    {
        return await _crawlerAppService.GetJiZhiHeZiMenuAndDetails();
    }
}