using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Rich.BookMark.Blazor;

[Dependency(ReplaceServices = true)]
public class BookMarkBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "BookMark";
}
