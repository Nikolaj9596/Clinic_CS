using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string? Avatar { get; set; }
        public DateTime DateBirthday { get; set; }
        public DateTime Created { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public Client()
        {
            Appointments = new List<Appointment>();
        }
    }
}
