using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? Avatar { get; set; }
        public DateTime DateBirthday { get; set; }
        public DateTime DateStartWork { get; set; }
        public DateTime Created { get; set; }

        public int ProfessionId { get; set; }
        public virtual Profession Profession { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
        public Doctor()
        {
            Appointments = new List<Appointment>();
        }
    }

}
