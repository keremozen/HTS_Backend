using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class InterpreterAppointment : Entity<int>
    {
        [Required]
        public int SalesMethodAndCompanionInfoId { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; }
        public string? Description { get; set; }
        public int? BranchId { get; set; }
        
        [ForeignKey("SalesMethodAndCompanionInfoId")]
        public SalesMethodAndCompanionInfo SalesMethodAndCompanionInfo { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }
}
