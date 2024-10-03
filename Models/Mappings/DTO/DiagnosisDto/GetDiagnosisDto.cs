namespace Clinic.Models.Mappings.DTO.DiagnosisDto
{
    public class GetDiagnosisDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

        public ClientDto Client { get; set; }
        public DoctorDto Doctor { get; set; }
        public ICollection<DiseaseDto> Diseases { get; set; }
    }

    public class ClientDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
    }

    public class DoctorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
    }

    public class DiseaseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
