namespace Clinic.Models.Mappings.DTO.DoctorAppointmentDto;

public class GetDoctorAppointmentDto
{
    public DateTime EndDataAppointment { get; set; }
    public DateTime StartDataAppointment { get; set; }
    public ClientDto Client { get; set; }
    public DoctorDto Doctor { get; set; }
    public DateTime Created { get; set; }

    public class ClientDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
    }

    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Avatar { get; set; }
    }

}
