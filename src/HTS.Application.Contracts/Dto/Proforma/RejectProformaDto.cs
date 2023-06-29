namespace HTS.Dto.Proforma;

public class RejectProformaDto
{
    public int Id { get; set; }
    public int? RejectReasonId { get; set; }
    public string RejectReason { get; set; }
}