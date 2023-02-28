using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Rich.BookMark.Crawler;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Rich.BookMark.Crawlers;

public class CrawlerMenuCache : ICrawlerMenuCache, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IDistributedCache _cache;
    private readonly ICrawlerMenuRepository _crawlerMenuRepository;


    public CrawlerMenuCache(
        IDistributedCache cache,
        ICrawlerMenuRepository crawlerMenuRepository,
        IJsonSerializer jsonSerializer)
    {
        _cache = cache;
        _crawlerMenuRepository = crawlerMenuRepository;
        _jsonSerializer = jsonSerializer;
    }


    public async Task<List<CrawlerMenu>> GetMenusAsync(SpiderSourceEnum spiderSource)
    {
        var cacheKey = $"crawler:menu:{spiderSource.ToString()}";

        var cacheData = await _cache.GetStringAsync(cacheKey);
        if (cacheData.IsNullOrWhiteSpace())
        {
            var menus = await _crawlerMenuRepository.GetMenus(spiderSource);
            if (menus.Any())
                cacheData = _jsonSerializer.Serialize(menus);
            _cache.SetStringAsync(cacheKey, cacheData);
        }

        return cacheData.IsNullOrWhiteSpace()
            ? new List<CrawlerMenu>()
            : _jsonSerializer.Deserialize<List<CrawlerMenu>>(cacheData);
    }
}