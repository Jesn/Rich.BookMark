using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rich.BookMark.Crawler;
using Rich.BookMark.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Rich.BookMark.Crawlers;

public class EfCoreCrawlerMenuRepository : EfCoreRepository<IBookMarkDbContext, CrawlerMenu, Guid>,
    ICrawlerMenuRepository
{
    public EfCoreCrawlerMenuRepository(IDbContextProvider<IBookMarkDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }
    
    public async Task DeleteMenuByDataSource(SpiderSourceEnum spiderSource,
        CancellationToken cancellationToken = default)
    {
        await (await GetDbSetAsync()).Where(x => x.SpiderSource == spiderSource)
            .ExecuteDeleteAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<CrawlerMenu>> GetMenus(SpiderSourceEnum spiderSource,
        CancellationToken cancellationToken = default)
    {
        var menus = await (await GetDbSetAsync()).Where(x => x.SpiderSource == spiderSource)
            .OrderBy(x => x.MenuId)
            .ToListAsync(GetCancellationToken(cancellationToken));
        return menus;
    }
}