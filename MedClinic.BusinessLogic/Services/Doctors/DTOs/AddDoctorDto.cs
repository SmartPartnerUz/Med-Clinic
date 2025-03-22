namespace MedClinic.BusinessLogic.Services;

public class AddDoctorDto
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; } = string.Empty;
    public Guid PositionId { get; set; }
    public Guid DoctorRoomId { get; set; }
    public Guid RoleId { get; set; }
    public Guid HospitalServiceId { get; set; }
    public int BedPercentage { get; set; }
    public double Salary { get; set; }
    public string? ImagePath { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; } = DateTime.UtcNow;
}