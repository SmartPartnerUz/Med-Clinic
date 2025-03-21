namespace MedClinic.BusinessLogic.Services;

public class UpdatePatientDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime BirthDate { get; set; }
}