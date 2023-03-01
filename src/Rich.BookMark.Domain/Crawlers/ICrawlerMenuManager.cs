using System.Collections.Generic;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;

namespace Rich.BookMark.Crawlers;

public interface ICrawlerMenuManager
{
    Task<List<CrawlerMenu>> GetMenusAsync(SpiderSourceEnum spiderSource);

    /// <summary>
    /// 批量新增菜单
    /// </summary>
    /// <param name="menus"></param>
    /// <returns></returns>
    Task InsertManyAsync(List<CrawlerMenu> menus);
    
    /// <summary>
    /// 根据爬虫类别删除对应的菜单
    /// </summary>
    /// <param name="spiderSource"></param>
    /// <returns></returns>
    Task DeleteMenuByDataSource(SpiderSourceEnum spiderSource);
}