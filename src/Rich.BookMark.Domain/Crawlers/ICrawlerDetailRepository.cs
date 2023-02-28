using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;
using Volo.Abp.Domain.Repositories;

namespace Rich.BookMark.Crawlers;

public interface ICrawlerDetailRepository : IBasicRepository<CrawlerDetail, Guid>
{

    Task<List<CrawlerDetail>> GetDetailsByDataSource(SpiderSourceEnum spiderSource);
    
    /// <summary>
    /// 获取指定菜单下的数据详情，如果menuId为空则获取所有数据
    /// </summary>
    /// <param name="menuId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<CrawlerDetail>> GetDetailsByMenuId(string menuId, CancellationToken cancellationToken = default);

    /// <summary>
    /// 根据爬虫来源删除该源下所有数据
    /// </summary>
    /// <param name="spiderSource"></param>
    /// <returns></returns>
    Task DeleteDetailByDataSource(SpiderSourceEnum spiderSource, CancellationToken cancellationToken = default);
}