using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HTS;

[Dependency(ReplaceServices = true)]
public class HTSBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "HTS";
}
