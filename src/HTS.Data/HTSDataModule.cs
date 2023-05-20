﻿using System.Linq;
using HTS.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.Modularity;

namespace HTS.Data
{
    [DependsOn(typeof(AbpEntityFrameworkCoreModule))]
    public class HTSDataModule : AbpModule
    {

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<AppDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpEntityOptions>(options =>
            {
                options.Entity<Patient>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Nationality)
                                                                        .Include(p => p.PhoneCountryCode)
                                                                        .Include(p => p.Gender)
                                                                        .Include(p => p.MotherTongue)
                                                                        .Include(p => p.SecondTongue)
                                                                        .Include(p => p.Creator)
                                                                        .Include(p => p.LastModifier)
                                                                        .Include(p => p.PatientTreatmentProcesses)
                                                                        .ThenInclude(t => t.TreatmentProcessStatus);
                });

                options.Entity<PatientTreatmentProcess>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(t => t.Creator)
                                                                        .Include(t => t.TreatmentProcessStatus);
                });

                options.Entity<ContractedInstitution>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(i => i.Nationality)
                                                                        .Include(i => i.PhoneCountryCode)
                                                                        .Include(i => i.ContractedInstitutionStaffs);
                });

                options.Entity<ContractedInstitutionStaff>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(s => s.PhoneCountryCode);
                });

                options.Entity<Hospital>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(o => o.City)
                                                                        .Include(p => p.PhoneCountryCode)
                                                                        .Include(h => h.HospitalStaffs)
                                                                        .ThenInclude(s => s.User);
                });

                options.Entity<HospitalStaff>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(s => s.User);
                });

                options.Entity<Process>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(p => p.ProcessType)
                                                                        .Include(p=>p.ProcessCosts)
                                                                        .Include(p=>p.ProcessRelations);
                });

                options.Entity<HospitalConsultation>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(hc => hc.Creator)
                        .Include(hc => hc.HospitalConsultationStatus);
                });

                options.Entity<HospitalResponse>(entityOptions =>
                {
                    entityOptions.DefaultWithDetailsFunc = query => query.Include(hr => hr.HospitalResponseBranches)
                        .Include(hr => hr.HospitalResponseProcesses)
                        .Include(hr => hr.HospitalResponseType)
                        .Include(hr => hr.HospitalResponseMaterials)
                        .Include(hr => hr.HospitalizationType);
                });

            });
        }
    }
}
