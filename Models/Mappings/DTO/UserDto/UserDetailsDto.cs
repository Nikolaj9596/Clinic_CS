using AutoMapper;
using Clinic.Models.Mapping;

namespace Clinic.Models.Mappings.DTO.UserDto;

public class UserDetailsDto : IMapWith<User>
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Surname { get; set; }
  public string Lastname { get; set; }
  public string DocumentNumber { get; set; }
  public Guid RoleId { get; set; }
  public string RoleName { get; set; }

  public void Mapping(Profile profile)
  {
    profile.CreateMap<User, UserDetailsDto>()
      .ForMember(t => t.RoleName, opt => opt.MapFrom(p => p.Role.Name));
  }
}
