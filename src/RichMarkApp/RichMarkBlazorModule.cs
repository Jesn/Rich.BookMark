using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rich.BookMark.EntityFrameworkCore;
using Rich.BookMark.Localization;
using Rich.BookMark;
using RichMarkApp.Data;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace RichMarkApp;

[DependsOn(
    typeof(BookMarkApplicationModule),
    typeof(BookMarkEntityFrameworkCoreModule),
    typeof(BookMarkHttpApiModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreSerilogModule)
)]
public class RichMarkBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(BookMarkResource),
                typeof(BookMarkDomainModule).Assembly,
                typeof(BookMarkDomainSharedModule).Assembly,
                typeof(BookMarkApplicationModule).Assembly,
                typeof(BookMarkApplicationContractsModule).Assembly,
                typeof(RichMarkBlazorModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.AddSingleton<JiZhiHeZiService>();

        ConfigureUrls(configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureSwaggerServices(context.Services);
    }


    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }

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
                options.FileSets.ReplaceEmbeddedByPhysical<RichMarkBlazorModule>(hostingEnvironment.ContentRootPath);
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


    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options => { options.AddMaps<RichMarkBlazorModule>(); });
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


        app.UseUnitOfWork();
        // app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "BookMark API"); });
        app.UseConfiguredEndpoints();
        

    }
}