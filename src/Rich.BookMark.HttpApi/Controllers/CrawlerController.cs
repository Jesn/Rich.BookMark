using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rich.BookMark.Crawler;
using Rich.BookMark.Crawler.BookMarkDto;
using Volo.Abp.AspNetCore.Mvc;

namespace Rich.BookMark.Controllers;

[Route("/api/crawler/")]
public class CrawlerController : AbpControllerBase
{
    private readonly ICrawlerAppService _crawlerAppService;

    public CrawlerController(ICrawlerAppService crawlerAppService)
    {
        _crawlerAppService = crawlerAppService;
    }

    /// <summary>
    /// 集知盒子数据采集
    /// </summary>
    [HttpGet]
    [Route("Jizhihezi")]
    public void Jizhihezi()
    {
        _crawlerAppService.Jizhihezi();
    }

    /// <summary>
    /// 集知盒子数据采集入库
    /// </summary>
    [HttpPost]
    [Route("SeedData")]
    public async Task SeedDataByJizhihezi()
    {
        var result = await _crawlerAppService.Jizhihezi();
        await _crawlerAppService.SeedData(result, SpiderSourceEnum.jizhihezi);
    }


    [HttpGet]
    [Route("GetJiZhiHeZiMenu")]
    public async Task<List<MenuDto>> GeJzHeZiMenu()
    {
        return await _crawlerAppService.GetJiZhiHeZiMenu();
    }

    [HttpGet]
    [Route("GeJzHeZiDetail/{menuId}")]
    public async Task<List<BookMarkDetailDto>> getJzHeZiiDetailes(string menuId)
    {
        return await _crawlerAppService.GetJiZhiHeZiDetailes(menuId);
    }
}