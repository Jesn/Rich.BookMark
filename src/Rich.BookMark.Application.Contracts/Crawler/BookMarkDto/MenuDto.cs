using System;
using System.Collections.Generic;

namespace Rich.BookMark.Crawler.BookMarkDto;


public class MenuDto
{
    public string ParentId { get; set; }
    public string MenuId { get; set; }
    
    public  string Icon { get; set; }
    public string Title { get; set; }

    public List<MenuDto> Menus { get; set; } = new List<MenuDto>();
}