
using HTS.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace HTS.Data
{
    [ReplaceDbContext(typeof(IIdentityDbContext))]
    [ConnectionStringName("Default")]
    public class AppDbContext : AbpDbContext<AppDbContext>, IIdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContractedInstitution> ContractedInstitutions { get; set; }
        public DbSet<ContractedInstitutionStaff> ContractedInstitutionStaffs { get; set; }
        public DbSet<ContractedInstitutionKind> ContractedInstitutionKinds { get; set; }
        public DbSet<ContractedInstitutionType> ContractedInstitutionTypes { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<AdditionalService> AdditionalServices { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<OperationStatus> OperationStatuses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalStaff> HospitalStaffs { get; set; }
        public DbSet<HospitalPricer> HospitalPricers { get; set; }
        public DbSet<HospitalUHBStaff> HospitalUHBStaffs { get; set; }
        public DbSet<HospitalConsultation> HospitalConsultations { get; set; }
        public DbSet<HospitalConsultationDocument> HospitalConsultationDocuments { get; set; }
        public DbSet<HospitalConsultationStatus> HospitalConsultationStatuses { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }     
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientAdmissionMethod> PatientAdmissionMethods { get; set; }
        public DbSet<PatientDocument> PatientDocuments { get; set; }
        public DbSet<PatientDocumentStatus> PatientDocumentStatuses { get; set; }
        public DbSet<PatientNote> PatientNotes { get; set; }
        public DbSet<PatientNoteStatus> PatientNoteStatuses { get; set; }
        public DbSet<PatientTreatmentProcess> PatientTreatmentProcesses { get; set; }
        public DbSet<SalesMethodAndCompanionInfo> SalesMethodAndCompanionInfos { get; set; }
        public DbSet<TreatmentProcessStatus> TreatmentProcessStatuses { get; set; }
        public DbSet<HospitalizationType> HospitalizationTypes { get; set; }
        public DbSet<HospitalResponse> HospitalResponses { get; set; }
        public DbSet<HospitalResponseType> HospitalResponseTypes { get; set; }
        public DbSet<HospitalResponseBranch> HospitalResponseBranches { get; set; }
        public DbSet<HospitalResponseProcess> HospitalResponseProcesses { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public DbSet<ProcessRelation> IncludingProcesses { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<ProcessCost> ProcessCosts { get; set; }
        public DbSet<ProcessType> ProcessTypes { get; set; }
        public DbSet<ProcessKind> ProcessKinds { get; set; }
        public DbSet<TreatmentType> TreatmentTypes { get; set; }
        public DbSet<RejectReason> RejectReasons { get; set; }
        
        public DbSet<Proforma>  Proformas { get; set; }
        public DbSet<ProformaProcess>  ProformaProcesses { get; set; }
        public DbSet<ProformaAdditionalService>  ProformaAdditionalServices { get; set; }
        public DbSet<ProformaNotIncludingService>  ProformaNotIncludingServices { get; set; }
        public DbSet<ProformaStatus> ProformaStatuses { get; set; }
        public  DbSet<ExchangeRateInformation> ExchangeRateInformations { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }
        public DbSet<PaymentReason> PaymentReasons { get; set; }
        public DbSet<PaymentKind> PaymentKinds { get; set; }
        public DbSet<PaymentDocument> PaymentDocuments { get; set; }
        public DbSet<PaymentStatus> PaymentStatuses { get; set; }
        public DbSet<ENabizProcess> ENabizProcesses { get; set; }

        public DbSet<TaskType>  TaskTypes { get; set; }
        public DbSet<HTSTask> HTSTasks { get; set; }
        
        public DbSet<IdentityUser> Users { get; set; }

        public DbSet<IdentityRole> Roles { get; set; }

        public DbSet<IdentityClaimType> ClaimTypes { get; set; }

        public DbSet<OrganizationUnit> OrganizationUnits { get; set; }

        public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }

        public DbSet<IdentityLinkUser> LinkUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureIdentity();

        }
    }
}
