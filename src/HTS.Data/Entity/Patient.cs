using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
namespace HTS.Data.Entity
{

    public class Patient : FullAuditedAggregateRoot<int>
    {
        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, StringLength(100)]
        public string Surname { get; set; }

        [StringLength(500)]
        public string PassportNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public bool IsActive { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        //public int NationalityId { get; set; }
        //public int GenderId { get; set; }
        //public int MotherTongueId { get; set; }
        //public int SecondTongueId { get; set; }
        public Nationality Nationality { get; set; }
        public Gender Gender { get; set; }
        public Language MotherTongueId { get; set; }
        public Language SecondTongueId { get; set; }


    }
}
