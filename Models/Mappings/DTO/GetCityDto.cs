using AutoMapper;
using Clinic.Models;
using Clinic.Models.Mapping;

namespace Clinic.Models.Mappings.DTO
{
  public class GetCityDto : IMapWith<City>
  {
    public Guid Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
      profile.CreateMap<City, GetCityDto>();
    }
  }
}
