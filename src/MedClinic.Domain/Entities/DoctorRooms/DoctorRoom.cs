using MedClinic.Domain.Entities.Doctors;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.DoctorRooms;

[Table("doctor_room")]
public class DoctorRoom : BaseEntity
{
    [Column("number")]
    public int Number { get; set; }

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}