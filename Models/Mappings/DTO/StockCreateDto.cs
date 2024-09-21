namespace Clinic.Models.Mappings.DTO;

public class StockCreateDto
{
  public string Name { get; set; }
  public string Address { get; set; }
  public Guid CityId { get; set; }
}
