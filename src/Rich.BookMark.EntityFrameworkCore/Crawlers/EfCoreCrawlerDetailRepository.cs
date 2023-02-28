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

public  class EfCoreCrawlerDetailRepository : EfCoreRepository<IBookMarkDbContext, CrawlerDetail, Guid>,
    ICrawlerDetailRepository
{

    public EfCoreCrawlerDetailRepository(IDbContextProvider<IBookMarkDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
    public async Task<List<CrawlerDetail>> GetDetailsByDataSource(SpiderSourceEnum spiderSource)
    {
        return await (await GetDbSetAsync()).ToListAsync();
    }

    public async Task<List<CrawlerDetail>> GetDetailsByMenuId(string menuId, CancellationToken cancellationToken)
    {
        return await (await GetDbSetAsync())
            .WhereIf(!menuId.IsNullOrWhiteSpace(), x => x.MenuId == menuId)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }
    
    public async Task DeleteDetailByDataSource(SpiderSourceEnum spiderSource,
        CancellationToken cancellationToken = default)
    {
        await (await GetDbSetAsync()).Where(x => x.SpiderSource == spiderSource)
            .ExecuteDeleteAsync(GetCancellationToken(cancellationToken));
    }
}