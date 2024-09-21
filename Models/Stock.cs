using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Stock
    {
        [Key]
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public Stock()
        {
            Invoices = new List<Invoice>();
        }
    }
}
