namespace Clinic.Models.Mappings.DTO.DoctorDto;

public class UpdateDoctorDto
{
  public string FirstName { get; set; }
  public string LastName { get; set; }
  public string MiddleName { get; set; }
  public DateTime DateBirthday { get; set; }
  public DateTime DateStartWork { get; set; }
  public int ProfessionId { get; set; }
  public string? Avatar { get; set; }
}
