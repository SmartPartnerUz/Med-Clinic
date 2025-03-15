using MedClinic.Domain.Entities.Doctors;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Roles;

[Table("role")]
public class Role : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
