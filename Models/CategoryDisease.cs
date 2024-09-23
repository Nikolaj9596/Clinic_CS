using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class CategoryDisease
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Disease> Diseases { get; set; }
        public CategoryDisease()
        {
            Diseases = new List<Disease>();
        }
    }
}
