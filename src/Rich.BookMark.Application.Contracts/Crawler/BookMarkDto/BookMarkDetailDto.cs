namespace Rich.BookMark.Crawler.BookMarkDto;

public class BookMarkDetailDto
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    public string MenuId { get; set; }

    /// <summary>
    /// 数据ID
    /// </summary>
    public string DataId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 源站点URL地址
    /// </summary>
    public string Url_Source { get; set; }

    /// <summary>
    /// 当前资源对应的采集站的地址
    /// </summary>
    public string Url_Spider { get; set; }

    public string Icon { get; set; }

    public string favicon { get; set; }
    public string Description { get; set; }
}