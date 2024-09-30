using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Diagnosis
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string? Description { get; set; }
        public DateTime Created { get; set; }
        public int ClientId { get; set; }
        public int DoctorId { get; set; }
        public Disease[] Diseases { get; set; }
        public virtual Client Client { get; set; }
        public virtual Doctor Doctor { get; set; }

    }
}
