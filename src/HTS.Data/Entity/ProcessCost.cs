using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace HTS.Data.Entity
{

    public class ProcessCost : IEntity<int>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public DateTime ValidityStartDate { get; set; }
        [Required]
        public DateTime ValidityEndDate { get; set; }
        [Required]
        public decimal HospitalPrice { get; set; }
        [Required]
        public decimal UshasPrice { get; set; }
        [Required]
        public bool IsActive { get; set; }
        
        [ForeignKey("ProcessId")]
        public Process Process { get; set; }

        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}