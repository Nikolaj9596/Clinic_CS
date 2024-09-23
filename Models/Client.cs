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
        public DateTime DateBirthday { get; set; }
        public DateTime Created { get; set; }
    }
}
