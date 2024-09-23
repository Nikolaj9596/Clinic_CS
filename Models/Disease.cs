using System.ComponentModel.DataAnnotations;

namespace Clinic.Models
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int CategoryDiseaseId { get; set; }
        public virtual CategoryDisease CategoryDisease { get; set; }
        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        public Disease()
        {
            Diagnosis = new List<Diagnosis>();
        }
    }
}
