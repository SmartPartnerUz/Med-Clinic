using MedClinic.Domain.Entities.DoctorRooms;
using MedClinic.Domain.Entities.HospitalServices;
using MedClinic.Domain.Entities.Positions;
using MedClinic.Domain.Entities.Roles;
using MedClinic.Domain.Entities.Users;

namespace MedClinic.BusinessLogic.Services;

public class DoctorDto
{
    public Guid Id { get; set; }
    public Position Position { get; set; } = null!;
    public DoctorRoom DoctorRoom { get; set; } = null!;
    public Role Role { get; set; } = null!;
    public User User { get; set; } = null!;
    public HospitalService HospitalService { get; set; } = null!;
    public int BedPercentage { get; set; }
    public double Salary { get; set; }
    public string? ImagePath { get; set; } = string.Empty;
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}