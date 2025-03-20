namespace MedClinic.BusinessLogic.Services;

public class HospitalServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public DateTime? UpdatedAt { get; set; }
}