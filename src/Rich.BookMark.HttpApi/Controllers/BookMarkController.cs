using Rich.BookMark.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Rich.BookMark.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BookMarkController : AbpControllerBase
{
    protected BookMarkController()
    {
        LocalizationResource = typeof(BookMarkResource);
    }
}
