using AutoMapper;
using Clinic.Models.Mapping;
using Clinic.Models.Mappings.DTO.StatusDto;
using Clinic.Models.Mappings.DTO.UserDto;

namespace Clinic.Models.Mappings.DTO.InvoiceDto;

public class InvoiceDetailsDto : IMapWith<Invoice>
{
    public Guid Id { get; set; }
    public StockDetailsDto Endpoint { get; set; }
    public UserDetailsDto Sender { get; set; }
    public UserDetailsDto Recipient { get; set; }
    public StatusDetailsDto Status { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Invoice, InvoiceDetailsDto>();
        profile.CreateMap<Stock, StockDetailsDto>();
        profile.CreateMap<User, UserDetailsDto>();
        profile.CreateMap<Status, StockDetailsDto>();
    }

}
