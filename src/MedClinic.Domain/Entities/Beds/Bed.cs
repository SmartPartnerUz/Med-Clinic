using MedClinic.Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Beds;

[Table("bed")]
public class Bed : BaseEntity
{
    [Column("number")]
    public int Number { get; set; } = 0;

    [Column("room_id")]
    public Guid RoomId { get; set; }

    [ForeignKey("RoomId")]
    public Room Room { get; set; } = null!;

    [Column("is_busy")]
    public bool IsBusy { get; set; }    
}
