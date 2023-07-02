using HTS.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HTS.Permissions;

public class HTSPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var htsGroup = context.AddGroup(HTSPermissions.GroupName);

        var managementPermission = htsGroup.AddPermission(HTSPermissions.Management, L("Permission:Management"));
        managementPermission.AddChild(HTSPermissions.HospitalManagement, L("Permission:HospitalManagement"));
        managementPermission.AddChild(HTSPermissions.NationalityManagement, L("Permission:NationalityManagement"));
        managementPermission.AddChild(HTSPermissions.CityManagement, L("Permission:CityManagement"));
        managementPermission.AddChild(HTSPermissions.LanguageManagement, L("Permission:LanguageManagement")); 
        managementPermission.AddChild(HTSPermissions.DocumentTypeManagement, L("Permission:DocumentTypeManagement"));
        managementPermission.AddChild(HTSPermissions.PatientAdmissionMethodManagement, L("Permission:PatientAdmissionMethodManagement"));
        managementPermission.AddChild(HTSPermissions.ContractedInstitutionManagement, L("Permission:ContractedInstitutionManagement"));
        managementPermission.AddChild(HTSPermissions.BranchManagement, L("Permission:BranchManagement"));
        managementPermission.AddChild(HTSPermissions.TreatmentTypeManagement, L("Permission:TreatmentTypeManagement"));
        managementPermission.AddChild(HTSPermissions.ProcessTypeManagement, L("Permission:ProcessTypeManagement"));
        managementPermission.AddChild(HTSPermissions.HospitalizationTypeManagement, L("Permission:HospitalizationTypeManagement"));
        managementPermission.AddChild(HTSPermissions.HospitalResponseTypeManagement, L("Permission:HospitalResponseTypeManagement"));
        managementPermission.AddChild(HTSPermissions.ProcessManagement, L("Permission:ProcessManagement"));
        managementPermission.AddChild(HTSPermissions.MaterialManagement, L("Permission:MaterialManagement"));
        managementPermission.AddChild(HTSPermissions.RejectReasonManagement, L("Permission:RejectReasonManagement"));
        managementPermission.AddChild(HTSPermissions.PaymentReasonManagement, L("Permission:PaymentReasonManagement"));

        var patientPermission = htsGroup.AddPermission(HTSPermissions.Patient, L("Permission:Patient"));
        var patientAccessPermission = patientPermission.AddChild(HTSPermissions.PatientAccess, L("Permission:PatientAccess"));
        patientAccessPermission.AddChild(HTSPermissions.PatientList, L("Permission:PatientList"));
        patientAccessPermission.AddChild(HTSPermissions.PatientManagement, L("Permission:PatientManagement"));
        patientAccessPermission.AddChild(HTSPermissions.PatientViewAll, L("Permission:PatientViewAll"));
        
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HTSResource>(name);
    }
}
