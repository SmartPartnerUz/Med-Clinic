using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Entities.Patients;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Users;

[Table("user")]
public class User : BaseEntity
{
    [Column("first_name")]
    public string? FirstName { get; set; } = string.Empty;

    [Column("last_name")]
    public string? LastName { get; set; } = string.Empty;

    [Column("phone_number")]
    public string? PhoneNumber { get; set; } = string.Empty;

    [Column("birth_date")]
    public DateTime? BirthDate { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Doctor? Doctor { get; set; }
    public Patient? Patient { get; set; }
}

