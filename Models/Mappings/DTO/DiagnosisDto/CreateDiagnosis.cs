namespace Clinic.Models.Mappings.DTO.DiagnosisDto
{
    public class CreateDiagnosisDto
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
        public ICollection<Guid> Diseases { get; set; }
    }
}
