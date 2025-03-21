namespace MedClinic.BusinessLogic.Services;

public class RoomDto
{
    public Guid Id { get; set; }
    public int Number { get; set; }
    public Guid StatusId { get; set; }
    public double Price { get; set; }
    public bool IsBusy { get; set; }
}