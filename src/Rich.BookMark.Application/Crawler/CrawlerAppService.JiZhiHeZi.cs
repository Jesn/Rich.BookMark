using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using Rich.BookMark.Crawler.BookMarkDto;

namespace Rich.BookMark.Crawler;

/// <summary>
/// 集知盒子数据采集
/// 
/// </summary>
public partial class CrawlerAppService
{
      /// <summary>
    /// 集知盒子数据采集
    /// </summary>
    /// <returns></returns>
    public async Task<ResultData> Jizhihezi()
    {
        var config = Configuration.Default.WithDefaultLoader();
        const string address = "https://www.Jizhihezi.com/";
        var document = await BrowsingContext.New(config).OpenAsync(address);

        var listMenu = GetJizhiheziMenu(document);

        var list_subMenu = listMenu?.SelectMany(x => x.Menus);

        var list_mark = new List<BookMarkDetailDto>();
        if (list_subMenu == null)
        {
            return new ResultData() { Menus = listMenu, Details = list_mark };
        }

        foreach (var menu in list_subMenu)
        {
            var cards = document.QuerySelectorAll($"#tab-{menu.MenuId}>div>.url-card");
            var favicon = string.Empty;
            foreach (var card in cards)
            {
                var urlSpider = card.QuerySelector("a")?.GetAttribute("href");
                var url_website = card.QuerySelector("a")?.GetAttribute("data-url");
                var dataId = card.QuerySelector("a")?.GetAttribute("data-id");
                var cardBody = card.QuerySelector("a>.card-body");

                var imagePoint = cardBody?.QuerySelector(".url-img>img");
                var icon = imagePoint?.GetAttribute("data-src");
                
                if (FaviconUrlRegex().Match(imagePoint.GetAttribute("onerror")).Success)
                    favicon = FaviconUrlRegex().Match(imagePoint.GetAttribute("onerror")).Groups[1].Value;

                var title = cardBody.QuerySelector(".url-info>div>strong").TextContent.Trim();
                var desc = cardBody.QuerySelector(".url-info>p").TextContent.Trim();

                list_mark.Add(new BookMarkDetailDto()
                {
                    MenuId = menu.MenuId,
                    DataId = dataId,
                    Icon = icon,
                    favicon = favicon,
                    Title = title,
                    Url_Source = url_website,
                    Url_Spider = urlSpider,
                    Description = desc
                });
            }
        }

        return new ResultData() { Menus = listMenu, Details = list_mark };
    }

    /// <summary>
    /// 抓取集知盒子左边菜单列表
    /// </summary>
    /// <param name="document"></param>
    /// <returns></returns>
    private static List<MenuDto> GetJizhiheziMenu(IDocument document)
    {
        var menuPoints = document.QuerySelectorAll(".sidebar-menu-inner>ul>li");
        var list_menu = new List<MenuDto>();
        var regex = MenuRegex();
        foreach (var menuPoint in menuPoints)
        {
            const string hrefNumberPattern = @"\d+";

            var a = menuPoint.QuerySelector("a");
            var href = a.GetAttribute("href");

            if (!regex.IsMatch(href)) continue;

            var match = MenuNumberRegex().Match(href);
            if (!match.Success) continue;
            var id = match.Value;

            var menu = new MenuDto()
            {
                ParentId = "0",
                MenuId = id,
                Title = RemoveEmptyRegex().Replace(a.TextContent, "")
            };

            // 二级菜单
            var subItemPoints = menuPoint.QuerySelectorAll("li>a");
            foreach (var subItemPoint in subItemPoints)
            {
                var subHref = subItemPoint.GetAttribute("href");

                var matchSub = MenuNumberRegex().Match(subHref);
                if (!matchSub.Success)
                {
                    continue;
                }

                var subId = matchSub.Value;
                var subTitle = subItemPoint.TextContent.Trim();

                // 剔除二级菜单和一级菜单相同的数据
                if (subId.Equals(menu.MenuId))
                {
                    continue;
                }

                menu.Menus.Add(new MenuDto()
                {
                    ParentId = menu.MenuId,
                    MenuId = subId,
                    Title = subTitle
                });
            }

            list_menu.Add(menu);
        }

        return list_menu;
    }

}