using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace HTS.Data.Entity
{

    public class Gender : IEntity<int>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsTransient()
        {
            if (EqualityComparer<int>.Default.Equals(Id, default(int)))
            {
                return true;
            }
            return Convert.ToInt32(Id) <= 0;
        }
        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}
