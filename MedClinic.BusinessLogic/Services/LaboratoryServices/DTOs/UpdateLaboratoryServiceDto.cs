namespace MedClinic.BusinessLogic.Services;

public class UpdateLaboratoryServiceDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid HospitalServiceId { get; set; }
}