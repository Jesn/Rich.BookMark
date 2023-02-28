using Rich.BookMark.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Rich.BookMark;

[DependsOn(
    typeof(BookMarkEntityFrameworkCoreTestModule)
    )]
public class BookMarkDomainTestModule : AbpModule
{

}
