using MedClinic.Domain.Entities.Doctors;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.DoctorRooms;

[Table("doctor_room")]
public class DoctorRoom : BaseEntity
{
    [Column("name")]
    public int Number { get; set; }
    public Doctor? Doctor { get; set; }
}