using AutoMapper;
using Clinic.Models.Mapping;

namespace Clinic.Models.Mappings.DTO;

public class StockDetailsDto : IMapWith<Stock>
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Address { get; set; }
  public Guid CityId { get; set; }
  public string CityName { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<Stock, StockDetailsDto>()
           .ForMember(t => t.CityName, opt => opt.MapFrom(p => p.City.Name));
  }

}
