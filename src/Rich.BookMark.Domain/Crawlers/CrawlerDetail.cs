using System;
using Rich.BookMark.Crawler;
using Volo.Abp.Domain.Entities;

namespace Rich.BookMark.Crawlers;

public class CrawlerDetail : Entity<Guid>
{
    public virtual SpiderSourceEnum SpiderSource { get; set; }

    /// <summary>
    /// 数据来源ID
    /// </summary>
    public virtual string DataId { get; set; }

    /// <summary>
    /// 资源站菜单ID
    /// </summary>
    public virtual string MenuId { get; set; }

    /// <summary>
    /// 标题
    /// </summary>
    public virtual string Title { get; set; }

    /// <summary>
    /// 源站点URL地址
    /// </summary>
    public virtual string Url_Source { get; set; }

    /// <summary>
    /// 当前资源对应的采集站的地址
    /// </summary>
    public virtual string Url_Spider { get; set; }

    /// <summary>
    /// 资源站Icon
    /// </summary>
    public virtual string Icon { get; set; }

    /// <summary>
    /// 资源站图片不存在，备用ICON
    /// </summary>
    public virtual string favicon { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public virtual string Description { get; set; }

    public CrawlerDetail()
    {
    }
}