namespace MedClinic.BusinessLogic.Services;

public class UpdateFirstViewOrderDto
{
    public Guid Id { get; set; }
    public int Queue { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
}