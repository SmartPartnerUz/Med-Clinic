namespace MedClinic.BusinessLogic.Services;

public class AddPayDeskDto
{
    public Guid ReceptionId { get; set; }
    public double Income { get; set; }
    public double Expense { get; set; }
}