using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }

        public City()
        {
            Stocks = new List<Stock>();
        }
    }
}
