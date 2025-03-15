using MedClinic.Domain.Entities.HospitalServices;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.LaboratoryServices;

[Table("laboratory_service")]
public class LaboratoryService : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public double Price { get; set; }

    [Column("hospital_service_id")]
    public Guid HospitalServiceId { get; set; }

    [ForeignKey("HospitalServiceId")]
    public HospitalService HospitalService { get; set; } = null!;

    [Column("update_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
}
