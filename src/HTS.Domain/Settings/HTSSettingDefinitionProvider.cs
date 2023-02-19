using Volo.Abp.Settings;

namespace HTS.Settings;

public class HTSSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(HTSSettings.MySetting1));
    }
}
