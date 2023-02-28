using System.IO;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rich.BookMark.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Server.LeptonXLiteTheme;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Rich.BookMark.Blazor;

[DependsOn(
    typeof(BookMarkApplicationModule),
    typeof(BookMarkEntityFrameworkCoreModule),
    typeof(BookMarkHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule),
    // typeof(AbpAccountWebOpenIddictModule),
    typeof(AbpAspNetCoreComponentsServerLeptonXLiteThemeModule),
    typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule),
    typeof(AbpCachingStackExchangeRedisModule)
    // typeof(AbpIdentityBlazorServerModule),
    // typeof(AbpTenantManagementBlazorServerModule),
    // typeof(AbpSettingManagementBlazorServerModule)
)]
public class BookMarkBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        // context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        // {
        //     options.AddAssemblyResource(
        //         typeof(BookMarkResource),
        //         typeof(BookMarkDomainModule).Assembly,
        //         typeof(BookMarkDomainSharedModule).Assembly,
        //         typeof(BookMarkApplicationModule).Assembly,
        //         typeof(BookMarkApplicationContractsModule).Assembly,
        //         typeof(BookMarkBlazorModule).Assembly
        //     );
        // });

        // PreConfigure<OpenIddictBuilder>(builder =>
        // {
        //     builder.AddValidation(options =>
        //     {
        //         options.AddAudiences("BookMark");
        //         options.UseLocalServer();
        //         options.UseAspNetCore();
        //     });
        // });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        // ConfigureAuthentication(context);
        ConfigureUrls(configuration);
        // ConfigureBundles();
        ConfigureAutoMapper();
        ConfigRedis();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
        // ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        // ConfigureRouter(context);
        // ConfigureMenu(context);
    }

    // private void ConfigureAuthentication(ServiceConfigurationContext context)
    // {
    //     context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults
    //         .AuthenticationScheme);
    // }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }

    private void ConfigRedis()
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "Blazor"; // 缓存Key前缀
        });
    }

    // private void ConfigureBundles()
    // {
    //     Configure<AbpBundlingOptions>(options =>
    //     {
    //         // MVC UI
    //         options.StyleBundles.Configure(
    //             LeptonXLiteThemeBundles.Styles.Global,
    //             bundle => { bundle.AddFiles("/global-styles.css"); }
    //         );
    //
    //         //BLAZOR UI
    //         options.StyleBundles.Configure(
    //             BlazorLeptonXLiteThemeBundles.Styles.Global,
    //             bundle =>
    //             {
    //                 bundle.AddFiles("/blazor-global-styles.css");
    //                 //You can remove the following line if you don't use Blazor CSS isolation for components
    //                 bundle.AddFiles("/Rich.BookMark.Blazor.styles.css");
    //             }
    //         );
    //     });
    // }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<BookMarkDomainSharedModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Rich.BookMark.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookMarkDomainModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Rich.BookMark.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookMarkApplicationContractsModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Rich.BookMark.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookMarkApplicationModule>(
                    Path.Combine(hostingEnvironment.ContentRootPath,
                        $"..{Path.DirectorySeparatorChar}Rich.BookMark.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<BookMarkBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "BookMark API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    // private void ConfigureMenu(ServiceConfigurationContext context)
    // {
    //     Configure<AbpNavigationOptions>(options => { options.MenuContributors.Add(new BookMarkMenuContributor()); });
    // }

    // private void ConfigureRouter(ServiceConfigurationContext context)
    // {
    //     Configure<AbpRouterOptions>(options => { options.AppAssembly = typeof(BookMarkBlazorModule).Assembly; });
    // }

    /// <summary>
    /// 创建自动API
    /// </summary>
    // private void ConfigureAutoApiControllers()
    // {
    //     Configure<AbpAspNetCoreMvcOptions>(options =>
    //     {
    //         options.ConventionalControllers.Create(typeof(BookMarkApplicationModule).Assembly);
    //     });
    // }
    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<BookMarkBlazorModule>(); });
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        // app.UseAuthentication();
        // app.UseAbpOpenIddictValidation();

        // if (MultiTenancyConsts.IsEnabled)
        // {
        //     app.UseMultiTenancy();
        // }

        app.UseUnitOfWork();
        // app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookMark API"); });
        app.UseConfiguredEndpoints();
    }
}