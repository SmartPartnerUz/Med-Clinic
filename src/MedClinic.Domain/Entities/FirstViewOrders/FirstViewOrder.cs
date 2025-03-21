﻿using MedClinic.Domain.Entities.Doctors;
using MedClinic.Domain.Entities.Patients;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedClinic.Domain.Entities.FirstViewOrders;

[Table("first_view_order")]
public class FirstViewOrder : BaseEntity
{
    [Column("queue")]
    public int Queue { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("doctor_id")]
    public Guid DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; } = null!;

    [Column("patient_id")]
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;
}
