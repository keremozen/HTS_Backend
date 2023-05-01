using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class HospitalResponseType : IEntity<int>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}