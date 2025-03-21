﻿using MedClinic.Domain.Entities.DoctorProfits;
using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Entities.HospitalServices;
using MedClinic.Domain.Entities.Patients;
using MedClinic.Domain.Entities.Rooms;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.Orders;

[Table("order")]
public class Order : BaseEntity
{
    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;

    [Column("room_id")]
    public Guid RoomId { get; set; }

    [ForeignKey("RoomId")]  
    public Room Room { get; set; } = null!;

    [Column("doctor_id")]
    public Guid DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; } = null!;

    [Column("hospital_service_id")]
    public Guid HospitalServiceId { get; set; }

    [ForeignKey("HospitalServiceId")]
    public HospitalService HospitalService { get; set; } = null!;

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime EndDate { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("date_price")]
    public double DatePrice { get; set; }

    [Column("total_price")]
    public double? TotalPrice { get; set; }

    [Column("is_persentage")]
    public bool IsPercentage { get; set; }

    [Column("bed_number")]
    public int BedNumber { get; set; }

    public ICollection<DoctorProfit> DoctorProfits { get; set; } = new List<DoctorProfit>();
}