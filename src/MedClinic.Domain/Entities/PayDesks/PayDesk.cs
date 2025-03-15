using MedClinic.Domain.Entities.Doctors;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.PayDesks;

[Table("pay_desk")]
public class PayDesk : BaseEntity
{
    [Column("reception_id")]
    public Guid ReceptionId { get; set; }

    [ForeignKey("ReceptionId")]
    public Doctor Reception { get; set; } = null!;

    [Column("income")]
    public double Income { get; set; }

    [Column("expense")]
    public double Expense { get; set; }
}
