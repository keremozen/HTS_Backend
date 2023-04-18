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
        htsGroup.AddPermission(HTSPermissions.BranchManagement, L("Permission:BranchManagement"));
        htsGroup.AddPermission(HTSPermissions.TreatmentTypeManagement, L("Permission:TreatmentTypeManagement"));
        htsGroup.AddPermission(HTSPermissions.ProcessTypeManagement, L("Permission:ProcessTypeManagement"));
        htsGroup.AddPermission(HTSPermissions.HospitalizationTypeManagement, L("Permission:HospitalizationTypeManagement"));
        htsGroup.AddPermission(HTSPermissions.HospitalResponseManagement, L("Permission:HospitalResponseManagement"));
        htsGroup.AddPermission(HTSPermissions.ProcessManagement, L("Permission:ProcessManagement"));
        htsGroup.AddPermission(HTSPermissions.ProcessCostManagement, L("Permission:ProcessCostManagement"));
        htsGroup.AddPermission(HTSPermissions.ProcessRelationManagement, L("Permission:ProcessRelationManagement"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HTSResource>(name);
    }
}
