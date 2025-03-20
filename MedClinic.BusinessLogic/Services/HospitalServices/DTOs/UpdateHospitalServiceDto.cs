namespace MedClinic.BusinessLogic.Services;

public class UpdateHospitalServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
}