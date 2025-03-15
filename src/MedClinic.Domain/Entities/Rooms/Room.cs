using MedClinic.Domain.Entities.Beds;
using MedClinic.Domain.Entities.Orders;
using MedClinic.Domain.Entities.Statuses;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Rooms;

[Table("room")]
public class Room : BaseEntity
{
    [Column("number")]
    public int Number { get; set; }

    [Column("status_id")]
    public int StatusId { get; set; }

    [ForeignKey("StatusId")]
    public Status Status { get; set; } = null!;

    [Column("price")]
    public double Price { get; set; }

    [Column("is_busy")]
    public bool IsBusy => Beds.All(i => i.IsBusy);

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Bed> Beds { get; set; } = new List<Bed>();
}
