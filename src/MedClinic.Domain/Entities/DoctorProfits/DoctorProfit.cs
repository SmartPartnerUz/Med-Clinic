using MedClinic.Domain.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.DoctorProfits;

[Table("doctor_profit")]
public class DoctorProfit : BaseEntity
{
    [Column("order_id")]
    public Guid OrderId { get; set; }

    [ForeignKey("OrderId")]
    public Order Order { get; set; } = null!;

    [Column("amount")]
    public double Amount { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
