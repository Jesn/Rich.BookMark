using AutoMapper;
using Rich.BookMark.Crawler.BookMarkDto;
using Rich.BookMark.Crawlers;
using Volo.Abp.AutoMapper;

namespace Rich.BookMark;

public class BookMarkApplicationAutoMapperProfile : Profile
{
    public BookMarkApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<MenuDto, CrawlerMenu>();
        CreateMap<CrawlerMenu, MenuDto>();

        CreateMap<BookMarkDetailDto, CrawlerDetail>();
        CreateMap<CrawlerDetail, BookMarkDetailDto>();
    }
}