namespace Clinic.Models.Mappings.DTO.AppointmentDto;

public class CreateAppointmentDto
{
    public DateTime EndDataAppointment { get; set; }
    public DateTime StartDataAppointment { get; set; }
    public Guid ClientId { get; set; }
    public Guid DoctorId { get; set; }
}
