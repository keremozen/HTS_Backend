using System.Linq;
using AutoMapper;
using HTS.Data.Entity;
using HTS.Dto.AdditionalService;
using HTS.Dto.Branch;
using HTS.Dto.City;
using HTS.Dto.ContractedInstitution;
using HTS.Dto.ContractedInstitutionKind;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.ContractedInstitutionType;
using HTS.Dto.Currency;
using HTS.Dto.DocumentType;
using HTS.Dto.ExchangeRateInformation;
using HTS.Dto.Gender;
using HTS.Dto.Hospital;
using HTS.Dto.HospitalConsultation;
using HTS.Dto.HospitalConsultationDocument;
using HTS.Dto.HospitalConsultationStatus;
using HTS.Dto.HospitalizationType;
using HTS.Dto.HospitalResponse;
using HTS.Dto.HospitalResponseBranch;
using HTS.Dto.HospitalResponseProcess;
using HTS.Dto.HospitalResponseType;
using HTS.Dto.HospitalStaff;
using HTS.Dto.HospitalUHBStaff;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Operation;
using HTS.Dto.OperationStatus;
using HTS.Dto.OperationType;
using HTS.Dto.Patient;
using HTS.Dto.PatientAdmissionMethod;
using HTS.Dto.PatientDocument;
using HTS.Dto.PatientDocumentStatus;
using HTS.Dto.PatientNote;
using HTS.Dto.PatientNoteStatus;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Dto.Payment;
using HTS.Dto.PaymentDocument;
using HTS.Dto.PaymentItem;
using HTS.Dto.PaymentKind;
using HTS.Dto.PaymentReason;
using HTS.Dto.Process;
using HTS.Dto.ProcessCost;
using HTS.Dto.ProcessKind;
using HTS.Dto.ProcessRelation;
using HTS.Dto.ProcessType;
using HTS.Dto.Proforma;
using HTS.Dto.ProformaAdditionalService;
using HTS.Dto.ProformaNotIncludingService;
using HTS.Dto.ProformaProcess;
using HTS.Dto.RejectReason;
using HTS.Dto.SalesMethodAndCompanionInfo;
using HTS.Dto.TreatmentProcessStatus;
using HTS.Dto.TreatmentType;

namespace HTS;

public class HTSApplicationAutoMapperProfile : Profile
{
    public HTSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Gender, GenderDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<AdditionalService, AdditionalServiceDto>();
        CreateMap<OperationType, OperationTypeDto>();
        CreateMap<OperationStatus, OperationStatusDto>();
        CreateMap<City, CityDto>();
        CreateMap<SaveCityDto, City>();
        CreateMap<Language, LanguageDto>();
        CreateMap<SaveLanguageDto, Language>();
        CreateMap<Nationality, NationalityDto>();
        CreateMap<SaveNationalityDto, Nationality>();
        CreateMap<DocumentType, DocumentTypeDto>();
        CreateMap<SaveDocumentTypeDto, DocumentType>();
        CreateMap<TreatmentProcessStatus, TreatmentProcessStatusDto>();
        CreateMap<HospitalizationType, HospitalizationTypeDto>();
     
        CreateMap<Branch, BranchDto>();
        CreateMap<SaveBranchDto, Branch>();
        CreateMap<ProcessType, ProcessTypeDto>();
        CreateMap<TreatmentType, TreatmentTypeDto>();
        CreateMap<SaveTreatmentTypeDto, TreatmentType>();
        CreateMap<RejectReason, RejectReasonDto>();
        CreateMap<SaveRejectReasonDto, RejectReason>();
        
        CreateMap<Patient, PatientDto>();
        CreateMap<SavePatientDto, Patient>();
        CreateMap<PatientNote, PatientNoteDto>();
        CreateMap<SavePatientNoteDto, PatientNote>();
        CreateMap<PatientNoteStatus, PatientNoteStatusDto>();
        CreateMap<PatientDocument, HospitalConsultationDocumentDto>();
        CreateMap<PatientDocument, PatientDocumentDto>();
        CreateMap<SavePatientDocumentDto, PatientDocument>();
        CreateMap<PatientDocumentStatus, PatientDocumentStatusDto>();
        CreateMap<SavePatientAdmissionMethodDto, PatientAdmissionMethod>();
        CreateMap<PatientAdmissionMethod, PatientAdmissionMethodDto>();
        CreateMap<SaveContractedInstitutionDto, ContractedInstitution>();
        CreateMap<ContractedInstitution, ContractedInstitutionDto>()
            .ForMember(x => x.Kind, opt => opt.MapFrom(o => o.ContractedInstitutionKind))
            .ForMember(x => x.Type, opt => opt.MapFrom(o => o.ContractedInstitutionType));
        CreateMap<SaveContractedInstitutionStaffDto, ContractedInstitutionStaff>();
        CreateMap<ContractedInstitutionStaff, ContractedInstitutionStaffDto>();
        CreateMap<SaveContractedInstitutionTypeDto, ContractedInstitutionType>();
        CreateMap<ContractedInstitutionType, ContractedInstitutionTypeDto>();
        CreateMap<SaveContractedInstitutionKindDto, ContractedInstitutionKind>();
        CreateMap<ContractedInstitutionKind, ContractedInstitutionKindDto>();
        CreateMap<SaveHospitalDto, Hospital>();
        CreateMap<Hospital, HospitalDto>();
        CreateMap<SaveHospitalStaffDto, HospitalStaff>();
        CreateMap<HospitalStaff, HospitalStaffDto>();
        CreateMap<SaveHospitalUHBStaffDto, HospitalUHBStaff>();
        CreateMap<HospitalUHBStaff, HospitalUHBStaffDto>();
        CreateMap<PatientTreatmentProcess, PatientTreatmentProcessDto>();
        CreateMap<SaveSalesMethodAndCompanionInfoDto, SalesMethodAndCompanionInfo>();
        CreateMap<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>();
        CreateMap<Process, ProcessDto>();
        CreateMap<SaveProcessDto, Process>();
        CreateMap<ProcessCost, ProcessCostDto>();
        CreateMap<SaveProcessCostDto, ProcessCost>();
        CreateMap<ProcessRelation, ProcessRelationDto>();
        CreateMap<SaveProcessRelationDto, ProcessRelation>();
        CreateMap<ProcessKind, ProcessKindDto>();
        CreateMap<SaveProcessKindDto, ProcessKind>();
        
        CreateMap<HospitalConsultation, HospitalConsultationDto>();
        CreateMap<SaveHospitalConsultationDto, HospitalConsultation>();
        CreateMap<HospitalConsultationDocument, HospitalConsultationDocumentDto>();
        CreateMap<SaveHospitalConsultationDocumentDto, HospitalConsultationDocument>();
        CreateMap<HospitalConsultationStatus, HospitalConsultationStatusDto>();
        
        CreateMap<HospitalResponse, HospitalResponseDto>();
        CreateMap<SaveHospitalResponseDto, HospitalResponse>();
        CreateMap<HospitalResponseType, HospitalResponseTypeDto>();
        CreateMap<HospitalResponseBranch, HospitalResponseBranchDto>();
        CreateMap<SaveHospitalResponseBranchDto, HospitalResponseBranch>();
        CreateMap<HospitalResponseProcess, HospitalResponseProcessDto>();
        CreateMap<SaveHospitalResponseProcessDto, HospitalResponseProcess>();
        CreateMap<Operation, OperationDto>();
        CreateMap<SaveOperationDto, Operation>();

        CreateMap<Proforma, ProformaDto>();
        CreateMap<Proforma, ProformaPricingListDto>();
        CreateMap<ProformaStatus, ProformaStatusDto>();
        CreateMap<SaveProformaDto, Proforma>();
        CreateMap<Proforma, ProformaListDto>().ForMember(x => x.Name,
            opt => opt.MapFrom(o => o.ProformaCode + " " + o.Creator.Name + " " + o.CreationDate));
        CreateMap<ProformaProcess, ProformaProcessDto>();
        CreateMap<SaveProformaProcessDto, ProformaProcess>();
        CreateMap<ProformaAdditionalService, ProformaAdditionalServiceDto>();
        CreateMap<SaveProformaAdditionalServiceDto, ProformaAdditionalService>();
        CreateMap<ProformaNotIncludingService, ProformaNotIncludingServiceDto>();
        CreateMap<SaveProformaNotIncludingServiceDto, ProformaNotIncludingService>();
        CreateMap<ExchangeRateInformation, ExchangeRateInformationDto>();

        CreateMap<PaymentReason, PaymentReasonDto>();
        CreateMap<PaymentKind, PaymentKindDto>();
        CreateMap<SavePaymentReasonDto, PaymentReason>();
        CreateMap<Payment, PaymentDto>();
        CreateMap<Payment, ListPaymentDto>()
            .ForMember(x => x.TotalPrice, opt => opt.MapFrom(o => o.PaymentItems.Sum(i => i.Price * i.ExchangeRate)));
        CreateMap<SavePaymentDto, Payment>();
        CreateMap<PaymentItem, PaymentItemDto>();
        CreateMap<SavePaymentItemDto, PaymentItem>();
        CreateMap<SavePaymentDocumentDto, PaymentDocument>();
    }
}
