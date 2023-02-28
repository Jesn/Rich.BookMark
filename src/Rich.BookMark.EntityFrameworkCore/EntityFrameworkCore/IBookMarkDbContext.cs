using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Rich.BookMark.EntityFrameworkCore;

[ConnectionStringName("Default")]
public interface IBookMarkDbContext:IEfCoreDbContext
{
    
}