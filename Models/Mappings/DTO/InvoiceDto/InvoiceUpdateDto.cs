namespace Clinic.Models.Mappings.DTO.InvoiceDto;

public class InvoiceUpdateDto
{
    public Guid Id { get; set; }
    public Guid EndpointId { get; set; }
    public Guid SenderId { get; set; }
    public Guid RecipientId { get; set; }
    public Guid StatusId { get; set; }

}
