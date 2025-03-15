using MedClinic.Domain.Entities.DoctorRooms;
using MedClinic.Domain.Entities.FirstViewOrders;
using MedClinic.Domain.Entities.HospitalServices;
using MedClinic.Domain.Entities.Orders;
using MedClinic.Domain.Entities.PayDesks;
using MedClinic.Domain.Entities.Positions;
using MedClinic.Domain.Entities.Roles;
using MedClinic.Domain.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Doctors;

[Table("doctor")]
public class Doctor : BaseEntity
{
    [Column("position_id")]
    public Guid PositionId { get; set; }

    [ForeignKey("PostionId")]
    public Position Position { get; set; } = null!;

    [Column("doctor_room_id")]
    public Guid DoctorRoomId { get; set; }

    [ForeignKey("DoctorRoomId")]
    public DoctorRoom DoctorRoom { get; set; } = null!;

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey("RoleId")]
    public Role Role { get; set; } = null!;

    [Column("user_id")]
    public Guid UserId { get; set; }

    [ForeignKey("UserId")]
    public User User { get; set; } = null!;

    [Column("hospital_service_id")]
    public Guid HospitalServiceId { get; set; }

    [ForeignKey("HospitalServiceId")]
    public HospitalService HospitalService { get; set; } = null!;

    [Column("bed_percentage")]
    public int BedPercentage { get; set; }

    [Column("salary")]
    public double Salary { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FirstViewOrder> FirstViewOrders { get; set; } = new List<FirstViewOrder>();
    public ICollection<PayDesk> PayDesks { get; set;} = new List<PayDesk>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}