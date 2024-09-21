using Clinic.Models.Mappings.DTO.InvoiceDto;

namespace Clinic.Models.Mappings.DTO.InvoiceDto
{
    public class InvoiceListDto
    {
        public IList<InvoiceDetailsDto> Invoices { get; set; }
    }
}


