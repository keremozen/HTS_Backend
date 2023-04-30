using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HTS.Dto.HospitalConsultationDocument;

namespace HTS.Dto.HospitalConsultation;

public class SaveHospitalConsultationDto
{
    [Required]
    public string Note { get; set; }
    [Required]
    public int PatientTreatmentProcessId { get; set; }
    [Required]
    public List<int> HospitalIds { get; set; }
    public List<SaveHospitalConsultationDocumentDto> HospitalConsultationDocuments { get; set; }
}