using Rich.BookMark.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Rich.BookMark.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BookMarkEntityFrameworkCoreModule),
    typeof(BookMarkApplicationContractsModule)
)]
public  class BookMarkDbMigratorModule : AbpModule
{
    // public override void OnApplicationInitialization(ApplicationInitializationContext context)
    // {
    //     // SeedData(context);
    // }

    // private async Task SeedData(ApplicationInitializationContext context)
    // {
    //     using var scope = context.ServiceProvider.CreateScope();
    //     await scope.ServiceProvider
    //         .GetRequiredService<IDataSeeder>()
    //         .SeedAsync();
    // }
}