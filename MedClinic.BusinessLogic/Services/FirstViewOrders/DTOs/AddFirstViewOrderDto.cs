namespace MedClinic.BusinessLogic.Services;

public class AddFirstViewOrderDto
{
    public int Queue { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
}