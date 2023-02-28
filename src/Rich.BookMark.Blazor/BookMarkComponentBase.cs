using Rich.BookMark.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Rich.BookMark.Blazor;

public abstract class BookMarkComponentBase : AbpComponentBase
{
    protected BookMarkComponentBase()
    {
        LocalizationResource = typeof(BookMarkResource);
    }
}