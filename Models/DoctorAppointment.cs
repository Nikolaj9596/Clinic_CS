using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime EndDataAppointment { get; set; }
        public DateTime StartDataAppointment { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
