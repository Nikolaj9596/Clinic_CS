using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public string DocumentNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Invoice> SendInvoices { get; set; }
        public virtual ICollection<Invoice> ReceiveInvoices { get; set; }
        public User()
        {
            SendInvoices = new List<Invoice>();
            ReceiveInvoices = new List<Invoice>();
        }

    }
}
