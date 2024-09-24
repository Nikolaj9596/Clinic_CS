using AutoMapper;

namespace Clinic.Models.Mappings.DTO.DiagnosisDto
{
    public class DiagnosisListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public string ClientName { get; set; }
        public string DoctorName { get; set; }
        public int NumberOfDiseases { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Client, DiagnosisListDto>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.FirstName));

            profile.CreateMap<Doctor, DiagnosisListDto>()
                .ForMember(dest => dest.DoctorName, opt => opt.MapFrom(src => src.FirstName));
        }
    }
}
