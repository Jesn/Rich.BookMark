using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Rich.BookMark.Crawler;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Rich.BookMark.Crawlers;

public class CrawlerDetailCache : ICrawlerDetailCache, ITransientDependency
{
    private readonly IDistributedCache _cache;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly ICrawlerDetailRepository _crawlerDetailRepository;

    public CrawlerDetailCache(
        IDistributedCache cache, 
        ICrawlerDetailRepository crawlerDetailRepository,
        IJsonSerializer jsonSerializer)
    {
        _cache = cache;
        _crawlerDetailRepository = crawlerDetailRepository;
        _jsonSerializer = jsonSerializer;
    }

    public async Task<List<CrawlerDetail>> GetDetailsAsync(SpiderSourceEnum spiderSource)
    {
        var cacheKey = $"crawler:detail:{spiderSource.ToString()}";

        var cacheData = await _cache.GetStringAsync(cacheKey);
        if (cacheData.IsNullOrWhiteSpace())
        {
            var details = await _crawlerDetailRepository.GetDetailsByDataSource(spiderSource);
            if (details.Any())
            {
                cacheData = _jsonSerializer.Serialize(details);
                _cache.SetStringAsync(cacheKey, cacheData);
            }
        }

        return cacheData.IsNullOrWhiteSpace()
            ? new List<CrawlerDetail>()
            : _jsonSerializer.Deserialize<List<CrawlerDetail>>(cacheData);
    }
}