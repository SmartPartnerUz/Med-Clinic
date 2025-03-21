namespace MedClinic.BusinessLogic.Services;

public class UpdateRoomDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public Guid StatusId { get; set; }
    public double Price { get; set; }
}