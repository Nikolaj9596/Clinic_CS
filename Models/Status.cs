using System.ComponentModel.DataAnnotations;
namespace Clinic.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }

        public Status()
        {
            Invoices = new List<Invoice>();
        }

    }
}
