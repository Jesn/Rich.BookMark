using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Rich.BookMark.Data;
using Volo.Abp.DependencyInjection;

namespace Rich.BookMark.EntityFrameworkCore;

public class EntityFrameworkCoreBookMarkDbSchemaMigrator
    : IBookMarkDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreBookMarkDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the BookMarkDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<BookMarkDbContext>()
            .Database
            .MigrateAsync();
    }
}
