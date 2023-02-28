using System;
using System.Collections.Generic;
using System.Text;
using Rich.BookMark.Localization;
using Volo.Abp.Application.Services;

namespace Rich.BookMark;

/* Inherit your application services from this class.
 */
public abstract class BookMarkAppService : ApplicationService
{
    protected BookMarkAppService()
    {
        LocalizationResource = typeof(BookMarkResource);
    }
}
