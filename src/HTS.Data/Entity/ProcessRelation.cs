using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;

namespace HTS.Data.Entity
{

    public class ProcessRelation : IEntity<int>
    {
        [Key, Required, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ProcessId { get; set; }
        [Required]
        public int ChildProcessId { get; set; }
        [ForeignKey("ProcessId")]
        public Process Process { get; set; }
        [ForeignKey("ChildProcessId")]
        public Process ChildProcess { get; set; }
        public object[] GetKeys()
        {
            return new object[] { Id };
        }
    }
}