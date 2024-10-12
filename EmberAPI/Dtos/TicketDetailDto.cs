namespace EmberAPI.Dtos;

public class TicketDetailDto
{
    public int TicketDetailsID { get; set; }
    public string? TicketMSG { get; set; }
    public DateTime MsgDate { get; set; }
    public int? sentByID { get; set; }
}