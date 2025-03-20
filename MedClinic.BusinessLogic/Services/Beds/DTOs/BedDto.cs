namespace MedClinic.BusinessLogic.Services;

public class BedDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public bool IsBusy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}