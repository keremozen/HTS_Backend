using System.Collections.Generic;
using HTS.Dto.ContractedInstitutionKind;
using HTS.Dto.ContractedInstitutionStaff;
using HTS.Dto.ContractedInstitutionType;
using HTS.Dto.Nationality;
using Volo.Abp.Application.Dtos;

namespace HTS.Dto.ContractedInstitution;

public class ContractedInstitutionDto : EntityDto<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int PhoneCountryCodeId { get; set; }
    public int NationalityId { get; set; }
    public string Site { get; set; }
    public string Address { get; set; }
    public int TypeId { get; set; }
    public int KindId { get; set; }
    public bool IsActive { get; set; }
    public NationalityDto PhoneCountryCode { get; set; }
    public NationalityDto Nationality { get; set; }
    public ContractedInstitutionKindDto Kind { get; set; }
    public ContractedInstitutionTypeDto Type { get; set; }
    public List<ContractedInstitutionStaffDto> ContractedInstitutionStaffs { get; set; }
}