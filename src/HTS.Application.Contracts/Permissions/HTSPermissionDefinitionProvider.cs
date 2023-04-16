using HTS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HTS.Permissions;

public class HTSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var htsGroup = context.AddGroup(HTSPermissions.GroupName);

        htsGroup.AddPermission(HTSPermissions.HospitalManagement, L("Permission:HospitalManagement"));
        htsGroup.AddPermission(HTSPermissions.NationalityManagement, L("Permission:NationalityManagement"));
        htsGroup.AddPermission(HTSPermissions.CityManagement, L("Permission:CityManagement"));
        htsGroup.AddPermission(HTSPermissions.LanguageManagement, L("Permission:LanguageManagement")); 
        htsGroup.AddPermission(HTSPermissions.DocumentTypeManagement, L("Permission:DocumentTypeManagement"));
        htsGroup.AddPermission(HTSPermissions.PatientAdmissionMethodManagement, L("Permission:PatientAdmissionMethodManagement"));
        htsGroup.AddPermission(HTSPermissions.ContractedInstitutionManagement, L("Permission:ContractedInstitutionManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HTSResource>(name);
    }
}
