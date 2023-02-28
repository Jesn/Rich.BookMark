using System.Collections.Generic;

namespace Rich.BookMark.Crawler.BookMarkDto;

public class ResultData
{
    public List<MenuDto> Menus = new List<MenuDto>();
    public List<BookMarkDetailDto> Details = new List<BookMarkDetailDto>();
}