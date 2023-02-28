using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Rich.BookMark.Data;

/* This is used if database provider does't define
 * IBookMarkDbSchemaMigrator implementation.
 */
public class NullBookMarkDbSchemaMigrator : IBookMarkDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
