using MedClinic.Domain.Entities.FirstViewOrders;
using MedClinic.Domain.Entities.Orders;
using MedClinic.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Patients;

[Table("patient")]
public class Patient : BaseEntity
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<FirstViewOrder> FirstViewsOrders { get; set; } = new List<FirstViewOrder>();
}
