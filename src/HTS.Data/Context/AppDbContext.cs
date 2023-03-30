
using HTS.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace HTS.Data
{
    [ConnectionStringName("Default")]
    public class AppDbContext : AbpDbContext<AppDbContext>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ContractedInstitution> ContractedInstitutions { get; set; }
        public DbSet<ContractedInstitutionStaff> ContractedInstitutionStaffs { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Hospital> Hospitals { get; set; }
        public DbSet<HospitalStaff> HospitalStaffs { get; set; }
        public DbSet<HospitalConsultation> HospitalConsultations { get; set; }
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
