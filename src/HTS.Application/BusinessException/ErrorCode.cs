namespace HTS.BusinessException;
public static class  ErrorCode
{
    public const string StaffAlreadyExist = "HTS:1";
    public const string DefaultStaffAlreadyExist = "HTS:2";
    public const string NationalityAndPassportNumberMustBeUnique = "HTS:3";
    public const string CreatorCanRevokePatientNote = "HTS:4";
    public const string CreatorCanRevokePatientDocument = "HTS:5";
    public const string PTPStatusNotValidToHospitalConsultation = "HTS:6";

}