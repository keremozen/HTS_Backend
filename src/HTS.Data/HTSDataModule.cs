using System.Linq;
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
                options.Entity<Patient>(orderOptions =>
                {
                 orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Nationality)
                                                                     .Include(p => p.PhoneCountryCode)
                                                                     .Include(p => p.Gender)
                                                                     .Include(p => p.MotherTongue)
                                                                     .Include(p => p.SecondTongue)
                                                                     .Include(p => p.Creator)
                                                                     .Include(p => p.LastModifier)
                                                                     .Include(p => p.PatientTreatmentProcesses)
                                                                     .ThenInclude(t => t.TreatmentProcessStatus);
                });

                options.Entity<PatientTreatmentProcess>(orderOptions => 
                {
                     orderOptions.DefaultWithDetailsFunc = query => query.Include(t => t.Creator)
                         .Include(t => t.TreatmentProcessStatus);
                });

            });
        }
    }
}
