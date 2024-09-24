namespace Clinic.Models.Mappings.DTO.ClientDto;

public class GetClientDto
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public DateTime DateBirthday { get; set; }
  public string Address { get; set; }
  public string? Avatar { get; set; }

}
