using System.Collections.Generic;
using System.Threading.Tasks;
using Rich.BookMark.Crawler;

namespace Rich.BookMark.Crawlers;

public interface ICrawlerDetailCache
{
    Task<List<CrawlerDetail>> GetDetailsAsync(SpiderSourceEnum spiderSource);
}