namespace MedClinic.BusinessLogic.Services;

public class UpdateOrderDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public Guid RoomId { get; set; }
    public Guid DoctorId { get; set; }
    public Guid HospitalServiceId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public double DatePrice { get; set; }
    public double? TotalPrice { get; set; }
    public bool IsPercentage { get; set; }
    public int BedNumber { get; set; }
}