using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Models
{
    public class Invoice
    {
        [Key]
        public Guid Id { get; set; }

        public virtual ICollection<Cargo> Cargoes { get; set; }
        [ForeignKey("endPoint")]
        public Guid EndpointId { get; set; }
        public virtual Stock Endpoint { get; set; }
        public Guid SenderId { get; set; }
        public virtual User Sender { get; set; }
        public Guid RecipientId { get; set; }
        public virtual User Recipient { get; set; }
        public Guid StatusId { get; set; }
        public virtual Status Status { get; set; }

        public Invoice()
        {
            Cargoes = new List<Cargo>();
        }
    }
}
