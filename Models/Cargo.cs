using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Cargo
    {
        [Key]
        public Guid Id { get; set; }
        public float Weight { get; set; }
        public Guid InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
