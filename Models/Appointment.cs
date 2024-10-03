using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Appointment
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime EndDataAppointment { get; set; }
        public DateTime StartDataAppointment { get; set; }
        public Guid ClientId { get; set; }
        public Guid DoctorId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
