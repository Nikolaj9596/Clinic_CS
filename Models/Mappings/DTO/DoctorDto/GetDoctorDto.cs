using AutoMapper;

namespace Clinic.Models.Mappings.DTO.DoctorDto;

public class GetDoctorDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public DateTime DateBirthday { get; set; }
    public DateTime DateStartWork { get; set; }
    public Guid Profession { get; set; }
    public string? Avatar { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Doctor, GetDoctorDto>()
          .ForMember(t => t.Profession, opt => opt.MapFrom(p => p.Profession.Name));
    }
}
