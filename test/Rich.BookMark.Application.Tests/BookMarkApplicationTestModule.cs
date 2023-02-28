using Volo.Abp.Modularity;

namespace Rich.BookMark;

[DependsOn(
    typeof(BookMarkApplicationModule),
    typeof(BookMarkDomainTestModule)
    )]
public class BookMarkApplicationTestModule : AbpModule
{

}
