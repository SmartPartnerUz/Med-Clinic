namespace MedClinic.BusinessLogic.Services;

public class AddRoomDto
{
    public int Number { get; set; }
    public Guid StatusId { get; set; }
    public double Price { get; set; }
}