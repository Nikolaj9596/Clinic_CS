namespace Clinic.Models.Mappings.DTO.DiseaseDto;

public class CreateDiseaseDto
{
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CategoryDiseaseId { get; set; }
}
