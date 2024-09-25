namespace Clinic.Models.Mappings.DTO.DoctorAppointmentDto;

public class CreateDoctorAppointmentDto
{
    public DateTime EndDataAppointment { get; set; }
    public DateTime StartDataAppointment { get; set; }
    public int ClientId { get; set; }
    public int DoctorId { get; set; }
}
