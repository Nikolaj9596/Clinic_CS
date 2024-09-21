using AutoMapper;
using Clinic.Models.Mapping;

namespace Clinic.Models.Mappings.DTO.CargoDto;

public class CargoDetailsDto : IMapWith<Cargo>
{
  public Guid Id { get; set; }
  public float Weight { get; set; }
  public Guid InvoiceId { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Cargo, CargoDetailsDto>();
  }

}
