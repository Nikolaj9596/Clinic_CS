using Clinic.Models.Mappings.DTO.ClientDto;
using Clinic.Models.Mappings.DTO.DoctorDto;
namespace Clinic.Models.Mappings.DTO.DiagnosisDto
{
    public class UpdateDiagnosisDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
        public ClientDto Client { get; set; }
        public DoctorDto Doctor { get; set; }
        public ICollection<Guid> Diseases { get; set; }
    }
}
