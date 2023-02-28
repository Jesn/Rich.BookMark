using Volo.Abp.Settings;

namespace Rich.BookMark.Settings;

public class BookMarkSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BookMarkSettings.MySetting1));
    }
}
