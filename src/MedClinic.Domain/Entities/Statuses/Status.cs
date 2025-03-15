using MedClinic.Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Statuses;

[Table("status")]
public class Status : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Room> Rooms { get; set; } = new List<Room>();
}
