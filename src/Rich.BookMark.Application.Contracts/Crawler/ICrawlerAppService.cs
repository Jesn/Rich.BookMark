using System.Collections.Generic;
using System.Threading.Tasks;
using Rich.BookMark.Crawler.BookMarkDto;

namespace Rich.BookMark.Crawler;

public interface ICrawlerAppService
{
    Task<ResultData> Jizhihezi();

    Task SeedData(ResultData data, SpiderSourceEnum spiderSource);


    Task<List<MenuDto>> GetJiZhiHeZiMenu();
    Task<List<BookMarkDetailDto>> GetJiZhiHeZiDetailes(string menuId);

    Task<ResultData> GetJiZhiHeZiMenuAndDetails();
}