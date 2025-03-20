namespace MedClinic.BusinessLogic.Services;

public class AddLaboratoryServiceDto
{
    public string Name { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid HospitalServiceId { get; set; }
}