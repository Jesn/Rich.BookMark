using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;
using Volo.Abp.Domain.Repositories;

namespace Rich.BookMark.Crawlers;

public interface ICrawlerMenuRepository : IBasicRepository<CrawlerMenu, Guid>
{
    /// <summary>
    /// 根据采集的数据来源获取对应的菜单数据
    /// </summary>
    /// <param name="spiderSource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<CrawlerMenu>> GetMenus(SpiderSourceEnum spiderSource,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据爬虫来源删除该源下所有数据
    /// </summary>
    /// <param name="spiderSource"></param>
    /// <returns></returns>
    Task DeleteMenuByDataSource(SpiderSourceEnum spiderSource,
        CancellationToken cancellationToken = default);
}