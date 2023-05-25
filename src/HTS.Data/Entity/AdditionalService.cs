using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace HTS.Data.Entity
{

    public class AdditionalService : IEntity<int>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required, StringLength(500)]
        public string Name { get; set; }

        [Required]
        public bool Day { get; set; }
        [Required]
        public bool Piece { get; set; }
        [Required]
        public bool RoomType { get; set; }
        [Required]
        public bool Companion { get; set; }
        
        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}
