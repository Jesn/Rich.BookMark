using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Rich.BookMark.Crawler;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Rich.BookMark.DbMigrator;

// public class CrawlerDataSeedContributorService : ITransientDependency
// {
//     private readonly ICrawlerAppService _crawlerAppService;
//
//     public CrawlerDataSeedContributorService(ICrawlerAppService crawlerAppService)
//     {
//         _crawlerAppService = crawlerAppService;
//     }
//
//
//     public async Task SeedAsync()
//     {
//         var result = await _crawlerAppService.Jizhihezi();
//         if (result.Menus.IsNullOrEmpty() && result.Details.IsNullOrEmpty()) return;
//
//         _crawlerAppService.InsertToDb(result, SpiderSourceEnum.jizhihezi);
//     }
// }