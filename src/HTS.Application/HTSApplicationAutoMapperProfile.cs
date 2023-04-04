using AutoMapper;
using HTS.Data.Entity;
using HTS.Dto;
using HTS.Dto.ContractedInstitution;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.DocumentType;
using HTS.Dto.Gender;
using HTS.Dto.Hospital;
using HTS.Dto.HospitalStaff;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.Patient;
using HTS.Dto.PatientAdmissionMethod;
using HTS.Dto.PatientNote;
using HTS.Dto.PatientNoteStatus;
using HTS.Dto.PatientTreatmentProcess;
using HTS.Dto.SalesMethodAndCompanionInfo;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace HTS;

public class HTSApplicationAutoMapperProfile : Profile
{
    public HTSApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Gender, GenderDto>();
        CreateMap<Language,LanguageDto>();
        CreateMap<SaveLanguageDto, Language>();
        CreateMap<Nationality,NationalityDto>();
        CreateMap<SaveNationalityDto, Nationality>();
        CreateMap<DocumentType, DocumentTypeDto>();
        CreateMap<SaveDocumentTypeDto, DocumentType>();

        CreateMap<Patient, PatientDto>();
        CreateMap<SavePatientDto, Patient>();
        CreateMap<PatientNote, PatientNoteDto>();
        CreateMap<SavePatientNoteDto, PatientNote>();
        CreateMap<PatientNoteStatus, PatientNoteStatusDto>(); 
        CreateMap<SavePatientAdmissionMethodDto, PatientAdmissionMethod>();
        CreateMap<PatientAdmissionMethod, PatientAdmissionMethodDto>();
        CreateMap<SaveContractedInstitutionDto, ContractedInstitution>();
        CreateMap<ContractedInstitution, ContractedInstitutionDto>();
        CreateMap<SaveContractedInstitutionStaffDto, ContractedInstitutionStaff>();
        CreateMap<ContractedInstitutionStaff, ContractedInstitutionStaffDto>();
        CreateMap<SaveHospitalDto, Hospital>();
        CreateMap<Hospital, HospitalDto>();
        CreateMap<SaveHospitalStaffDto, HospitalStaff>();
        CreateMap<HospitalStaff, HospitalStaffDto>();
        CreateMap<PatientTreatmentProcess, PatientTreatmentProcessDto>();
        CreateMap<SaveSalesMethodAndCompanionInfoDto, SalesMethodAndCompanionInfo>();
        CreateMap<SalesMethodAndCompanionInfo, SalesMethodAndCompanionInfoDto>();
    }
}
