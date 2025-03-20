namespace MedClinic.BusinessLogic.Services;

public class FirstViewOrderDto
{
    public Guid Id { get; set; }
    public int Queue { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime? UpdatedAt { get; set; }
}