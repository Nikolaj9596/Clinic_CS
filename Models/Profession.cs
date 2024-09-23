using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Profession 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public Profession()
        {
            Doctors = new List<Doctor>();
        }
    }
}
