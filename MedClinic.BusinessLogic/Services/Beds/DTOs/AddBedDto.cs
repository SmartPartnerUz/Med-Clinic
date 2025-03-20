namespace MedClinic.BusinessLogic.Services;

public class AddBedDto
{
    public int Number { get; set; }
    public Guid RoomId { get; set; }
    public bool? IsBusy { get; set; } = false;
}