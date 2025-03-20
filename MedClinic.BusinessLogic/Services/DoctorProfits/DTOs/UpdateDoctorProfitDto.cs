namespace MedClinic.BusinessLogic.Services;

public class UpdateDoctorProfitDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public double Amount { get; set; }
}

