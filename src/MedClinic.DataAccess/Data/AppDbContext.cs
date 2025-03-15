using MedClinic.Domain.Entities.Beds;
using MedClinic.Domain.Entities.DoctorProfits;
using MedClinic.Domain.Entities.DoctorRooms;
using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Entities.FirstViewOrders;
using MedClinic.Domain.Entities.HospitalServices;
using MedClinic.Domain.Entities.LaboratoryServices;
using MedClinic.Domain.Entities.Orders;
using MedClinic.Domain.Entities.Patients;
using MedClinic.Domain.Entities.PayDesks;
using MedClinic.Domain.Entities.Positions;
using MedClinic.Domain.Entities.Roles;
using MedClinic.Domain.Entities.Rooms;
using MedClinic.Domain.Entities.Statuses;
using MedClinic.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace MedClinic.DataAccess.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<Position> Positions { get; set; }
    DbSet<DoctorRoom> DoctorRooms { get; set; }
    DbSet<Doctor> Doctors { get; set; }
    DbSet<HospitalService> HospitalServices { get; set; }
    DbSet<LaboratoryService> LaboratoryServices { get; set; }
    DbSet<FirstViewOrder> FirstViewOrders { get; set; }
    DbSet<PayDesk> PayDesks { get; set; }
    DbSet<Patient> Patients { get; set; }
    DbSet<DoctorProfit> DoctorProfits { get; set; }
    DbSet<Status> Status { get; set; }
    DbSet<Order> Orders { get; set; }
    DbSet<Room> Rooms { get; set; }
    DbSet<Bed> Beds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user"); // Maps to "user" table

            entity.HasKey(u => u.Id); // Primary Key

            // One-to-One Relationship with Doctor
            entity.HasOne(u => u.Doctor)
                .WithOne(d => d.User)
                .HasForeignKey<Doctor>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-One Relationship with Patient
            entity.HasOne(u => u.Patient)
                .WithOne(p => p.User)
                .HasForeignKey<Patient>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
