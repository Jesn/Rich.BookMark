using System.Text.RegularExpressions;

namespace Rich.BookMark.Crawler;

public partial class CrawlerAppService
{
    /// <summary>
    /// 验证菜单是否满足条件
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("#term-\\d+")]
    private static partial Regex MenuRegex();

    /// <summary>
    /// 菜单数字验证
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("\\d+")]
    private static partial Regex MenuNumberRegex();

    /// <summary>
    /// 移除空格和换行
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("\\s+")]
    private static partial Regex RemoveEmptyRegex();

    /// <summary>
    /// 如果站点图标加载失败，则获取onerror atr里面的url链接地址
    /// </summary>
    /// <returns></returns>
    [GeneratedRegex("javascript:this.src='(.*?)'")]
    private static partial Regex FaviconUrlRegex();
}