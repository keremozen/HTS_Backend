using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace HTS.Data.Entity
{

    public class Operation : FullAuditedEntityWithUser<int, IdentityUser>
    {
        [Required]
        public int HospitalResponseId { get; set; }
       
        
        public int TreatmentTypeId { get; set; }
        
        public int OperationTypeId { get; set; }
        public int? PatientTreatmentProcessId { get; set; }
        public int? HospitalId { get; set; }
        public int OperationStatusId { get; set; }

        [ForeignKey("HospitalResponseId")]
        public HospitalResponse HospitalResponse { get; set; }
        [ForeignKey("TreatmentTypeId")]
        public TreatmentType? TreatmentType { get; set; }
        [ForeignKey("OperationTypeId")]
        public OperationType OperationType { get; set; }
        [ForeignKey("OperationStatusId")]
        public OperationStatus OperationStatus { get; set; }
        [ForeignKey("AppointedInterpreterId")]
        public IdentityUser? AppointedInterpreter { get; set; }
        
        [ForeignKey("HospitalId")]
        public Hospital? Hospital { get; set; }
        [ForeignKey("PatientTreatmentProcessId")]
        public PatientTreatmentProcess? PatientTreatmentProcess { get; set; }
        public virtual ICollection<Proforma> Proformas { get; set; }
        
    }
}
