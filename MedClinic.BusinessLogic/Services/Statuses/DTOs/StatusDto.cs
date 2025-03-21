namespace MedClinic.BusinessLogic.Services;

public class StatusDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; }
}
