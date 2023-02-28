using System.Collections.Generic;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;

namespace Rich.BookMark.Crawlers;

public interface ICrawlerMenuCache
{
    Task<List<CrawlerMenu>> GetMenusAsync(SpiderSourceEnum spiderSource);
}