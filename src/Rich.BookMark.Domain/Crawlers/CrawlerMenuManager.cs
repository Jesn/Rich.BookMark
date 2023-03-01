using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using Microsoft.Extensions.Caching.Distributed;
using Rich.BookMark.Crawler;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace Rich.BookMark.Crawlers;

public class CrawlerMenuManager : ICrawlerMenuManager, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IDistributedCache _cache;
    private readonly ICrawlerMenuRepository _crawlerMenuRepository;


    public CrawlerMenuManager(
        IDistributedCache cache,
        ICrawlerMenuRepository crawlerMenuRepository,
        IJsonSerializer jsonSerializer)
    {
        _cache = cache;
        _crawlerMenuRepository = crawlerMenuRepository;
        _jsonSerializer = jsonSerializer;
    }

    private const string cacheKeyFormat = "crawler:menu:{0}";

    public async Task<List<CrawlerMenu>> GetMenusAsync(SpiderSourceEnum spiderSource)
    {
        var cacheKey = CalculateCacheKey(spiderSource);
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

    /// <summary>
    /// 批量插入数据
    /// </summary>
    /// <param name="menus"></param>
    /// <exception cref="DataException"></exception>
    public async Task InsertManyAsync(List<CrawlerMenu> menus)
    {
        if (menus.IsNullOrEmpty())
        {
            throw new DataException("插入数据不能为空");
        }

        var cacheKey = CalculateCacheKey(menus[0].SpiderSource);

        await _crawlerMenuRepository.InsertManyAsync(menus);
        var cacheData = _jsonSerializer.Serialize(menus);
        await _cache.SetStringAsync(cacheKey, cacheData);
    }

    /// <summary>
    /// 删除菜单以及缓存
    /// </summary>
    /// <param name="spiderSource"></param>
    public async Task DeleteMenuByDataSource(SpiderSourceEnum spiderSource)
    {
        var cacheKey = CalculateCacheKey(spiderSource);
        await _crawlerMenuRepository.DeleteMenuByDataSource(spiderSource);
        await _cache.RemoveAsync(cacheKey);
    }

    /// <summary>
    /// 获取Cache名称
    /// </summary>
    /// <param name="spiderSource"></param>
    /// <returns></returns>
    private static string CalculateCacheKey(SpiderSourceEnum spiderSource) =>
        string.Format(cacheKeyFormat, spiderSource.ToString());
}