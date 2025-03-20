using MedClinic.Domain.Entities.Orders;

namespace MedClinic.BusinessLogic.Services;

public class DoctorProfitDto
{
    public Guid Id { get; set; }
    public Order Order { get; set; } = null!;
    public double Amount { get; set; }
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
