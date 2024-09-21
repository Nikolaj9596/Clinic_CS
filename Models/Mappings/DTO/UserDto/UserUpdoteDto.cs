using Clinic.Models.Mapping;

namespace Clinic.Models.Mappings.DTO.UserDto;

public class UserUpdoteDto
{
  public string Name { get; set; }
  public string Surname { get; set; }
  public string Lastname { get; set; }
  public string DocumentNumber { get; set; }
  public string Email { get; set; }
  public Guid? RoleId { get; set; }

}
