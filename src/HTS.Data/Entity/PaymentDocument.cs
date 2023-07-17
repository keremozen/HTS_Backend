using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class PaymentDocument : Entity<int>
    {
        [Required]
        public int PaymentId { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string FilePath { get; set; }
        public string? SignedFileName { get; set; }
        public string? SignedFilePath { get; set; }
        [Required]
        public bool IsSigned { get; set; }
        [ForeignKey("PaymentId")]
        public Payment Payment { get; set; }
      
    }
}
