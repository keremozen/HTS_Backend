namespace HTS.BusinessException;
public static class  ErrorCode
{
    public const string StaffAlreadyExist = "HTS:1";
    public const string DefaultStaffAlreadyExist = "HTS:2";
    public const string NationalityAndPassportNumberMustBeUnique = "HTS:3";
    public const string CreatorCanRevokePatientNote = "HTS:4";
    public const string CreatorCanRevokePatientDocument = "HTS:5";
    public const string PtpStatusNotValidToHospitalConsultation = "HTS:6";
    public const string RelationalDataIsMissing = "HTS:7";
    public const string HospitalAlreadyResponsed = "HTS:8";
    public const string RequiredFieldsMissingForSuitableForTreatment = "HTS:9";
    public const string HospitalResponseTypeNotValidToApprove = "HTS:10";
    public const string HospitalResponseTypeNotValidToReject = "HTS:11";
    public const string AnotherHospitalResponseIsApprovedInSameRowNumber = "HTS:12";
    public const string TreatmentNumberCouldNotBeGenerated = "HTS:13";
    public const string NoExchangeRateInformation = "HTS:14";
    public const string ExchangeRateInformationNotMatch = "HTS:15";
    public const string ProformaStatusNotValid = "HTS:16";
    public const string ProformaAdditionalServiceNotValid = "HTS:17";
    public const string InvalidProcessInProforma = "HTS:18";
    public const string InvalidProcessUnitPriceInProforma = "HTS:19";
    public const string InvalidCalculationsInProforma = "HTS:20";
    public const string BadRequest = "HTS:20";
    public const string LastProformaVersionCanBeApprovedRejected = "HTS:21";
    public const string LastProformaVersionCanBeOperated = "HTS:22";
    public const string RequiredFieldsMissing = "HTS:23";
    public const string OperationStatusNotValid = "HTS:24";

}