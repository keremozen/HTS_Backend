namespace HTS.BusinessException;
public static class  ErrorCode
{
    public const string StaffAlreadyExist = "HTS:1";
    public const string DefaultStaffAlreadyExist = "HTS:2";
    public const string NationalityAndPassportNumberMustBeUnique = "HTS:3";
    public const string CreatorCanRevokePatientNote = "HTS:4";
    public const string CreatorCanRevokePatientDocument = "HTS:5";
    public const string PTPStatusNotValidToHospitalConsultation = "HTS:6";
    public const string RelationalDataIsMissing = "HTS:7";
    public const string HospitalAlreadyResponsed = "HTS:8";
    public const string RequiredFieldsMissingForSuitableForTreatment = "HTS:9";
    public const string HospitalResponseTypeNotValidToApprove = "HTS:10";
    public const string HospitalResponseTypeNotValidToReject = "HTS:11";

}