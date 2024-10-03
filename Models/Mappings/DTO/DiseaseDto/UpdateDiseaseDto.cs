namespace Clinic.Models.Mappings.DTO.DiseaseDto;

public class UpdateDiseaseDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid CategoryDiseaseId { get; set; }
}
