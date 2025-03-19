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
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<DoctorRoom> DoctorRooms { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<HospitalService> HospitalServices { get; set; }
    public DbSet<LaboratoryService> LaboratoryServices { get; set; }
    public DbSet<FirstViewOrder> FirstViewOrders { get; set; }
    public DbSet<PayDesk> PayDesks { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<DoctorProfit> DoctorProfits { get; set; }
    public DbSet<Status> Status { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Bed> Beds { get; set; }

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

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("role"); // Maps to "role" table

            entity.HasKey(r => r.Id); // Primary Key

            entity.HasMany(r => r.Doctors)
                  .WithOne(d => d.Role)
                  .HasForeignKey(dr => dr.RoleId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("position"); // Maps to "position" table

            entity.HasKey(p => p.Id); // Primary Key

            entity.HasMany(p => p.Doctors)
                  .WithOne(d => d.Position)
                  .HasForeignKey(dp => dp.PositionId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DoctorRoom>(entity =>
        {
            entity.ToTable("doctor_room"); // Table Name

            entity.HasKey(dr => dr.Id); // Primary Key

            // One-to-Many Relationship: A DoctorRoom can have multiple Doctors
            entity.HasMany(dr => dr.Doctors)
                .WithOne(d => d.DoctorRoom)
                .HasForeignKey(d => d.DoctorRoomId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents cascade delete
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.ToTable("doctor");

            entity.HasKey(d => d.Id);

            // Relationships
            entity.HasMany(d => d.FirstViewOrders)
                  .WithOne(f => f.Doctor)
                  .HasForeignKey(df => df.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(d => d.PayDesks)
                  .WithOne(p => p.Reception)
                  .HasForeignKey(pd => pd.ReceptionId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(d => d.Orders)
                  .WithOne(o => o.Doctor)
                  .HasForeignKey(od => od.DoctorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<HospitalService>(entity =>
        {
            entity.ToTable("hospital_service"); // Table Name

            entity.HasKey(hs => hs.Id); // Primary Key

            // Relationships
            // One-to-Many: HospitalService -> Doctors
            entity.HasMany(hs => hs.Doctors)
                .WithOne(d => d.HospitalService)
                .HasForeignKey(d => d.HospitalServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: HospitalService -> LaboratoryServices
            entity.HasMany(hs => hs.LaboratoryServices)
                .WithOne(ls => ls.HospitalService)
                .HasForeignKey(ls => ls.HospitalServiceId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many: HospitalService -> Orders
            entity.HasMany(hs => hs.Orders)
                .WithOne(o => o.HospitalService)
                .HasForeignKey(o => o.HospitalServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<LaboratoryService>(entity =>
        {
            entity.ToTable("laboratory_service"); // Table Name

            entity.HasKey(ls => ls.Id); // Primary Key
        });

        modelBuilder.Entity<FirstViewOrder>(entity =>
        {
            entity.ToTable("first_view_order"); // Table Name

            entity.HasKey(fvo => fvo.Id); // Primary Key
        });

        modelBuilder.Entity<PayDesk>(entity =>
        {
            entity.ToTable("pay_desk"); // Table Name

            entity.HasKey(pd => pd.Id); // Primary Key
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("patient"); // Table Name

            entity.HasKey(p => p.Id); // Primary Key

            // Relationships
            // One-to-Many: Patient -> Order
            entity.HasMany(p => p.Orders)
                .WithOne(o => o.Patient)
                .HasForeignKey(op => op.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(p => p.FirstViewsOrders)
                  .WithOne(o => o.Patient)
                  .HasForeignKey(op => op.PatientId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<DoctorProfit>(entity =>
        {
            entity.ToTable("doctor_profit"); // Table Name

            entity.HasKey(dp => dp.Id); // Primary Key
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("order"); // Table Name

            entity.HasKey(o => o.Id); // Primary Key

            // Relationships
            // One-to-Many: Order -> DoctorProfit
            entity.HasMany(p => p.DoctorProfits)
                .WithOne(o => o.Order)
                .HasForeignKey(op => op.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("status"); // Table Name

            entity.HasKey(s => s.Id); // Primary Key

            // Relationships
            // One-to-Many: Status -> Rooms
            entity.HasMany(s => s.Rooms)
                .WithOne(r => r.Status)
                .HasForeignKey(r => r.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.ToTable("room"); // Table Name

            entity.HasKey(r => r.Id); // Primary Key

            // Relationships
            // One-to-Many: Room -> Beds
            entity.HasMany(r => r.Beds)
                .WithOne(b => b.Room)
                .HasForeignKey(b => b.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Room -> Orders
            entity.HasMany(r => r.Orders)
                .WithOne(o => o.Room)
                .HasForeignKey(o => o.RoomId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Bed>(entity =>
        {
            entity.ToTable("bed"); // Table Name

            entity.HasKey(b => b.Id); // Primary Key
        });
    }
}
