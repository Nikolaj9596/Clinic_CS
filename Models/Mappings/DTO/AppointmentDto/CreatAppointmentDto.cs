namespace Clinic.Models.Mappings.DTO.AppointmentDto;

public class CreateAppointmentDto
{
    public DateTime EndDataAppointment { get; set; }
    public DateTime StartDataAppointment { get; set; }
    public int ClientId { get; set; }
    public int DoctorId { get; set; }
}
