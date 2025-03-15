using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Entities.LaboratoryServices;
using MedClinic.Domain.Entities.Orders;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.HospitalServices;

[Table("hospital_service")]
public class HospitalService : BaseEntity
{
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("price")]
    public double Price { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
    public ICollection<LaboratoryService> LaboratoryServices { get; set; } = new List<LaboratoryService>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
