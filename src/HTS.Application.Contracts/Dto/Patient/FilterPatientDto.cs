using System.Collections.Generic;
using HTS.Dto.Gender;
using HTS.Dto.Language;
using HTS.Dto.Nationality;
using HTS.Dto.PatientTreatmentProcess;

namespace HTS.Dto.Patient;

public class FilterPatientDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string PassportNumber { get; set; }
    public List<int> PhoneCountryCodeIds { get; set; }
    public List<int> NationalityIds { get; set; }
    public List<int> GenderIds { get; set; }
    public List<int> MotherTongueIds { get; set; }
    public List<int> SecondTongueIds { get; set; }
    public List<int> PatientTreatmentProcessStatusIds { get; set; }
}