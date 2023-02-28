using System;
using System.Collections.Generic;
using Rich.BookMark.Crawler;
using Volo.Abp.Domain.Entities;

namespace Rich.BookMark.Crawlers;

/// <summary>
///
/// </summary>
public class CrawlerMenu : Entity<Guid>
{
    public virtual string MenuId { get; set; }
    public virtual string ParentId { get; set; }
    public virtual string Title { get; set; }
    public virtual string Icon { get; set; }
    public virtual int Sort { get; set; }
    public virtual SpiderSourceEnum SpiderSource { get; set; }

    public CrawlerMenu()
    {
    }
}