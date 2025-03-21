namespace MedClinic.BusinessLogic.Services;

public class UpdatePayDeskDto
{
    public Guid Id { get; set; }
    public Guid ReceptionId { get; set; }
    public double Income { get; set; }
    public double Expense { get; set; }
}