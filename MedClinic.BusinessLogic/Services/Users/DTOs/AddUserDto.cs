namespace MedClinic.BusinessLogic.Services;

public class AddUserDto
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; } = DateTime.UtcNow;
}