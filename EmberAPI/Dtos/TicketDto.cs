namespace EmberAPI.Dtos;

public class TicketDto
{
    public int ticketID { get; set; }
    public string? TicketTitle { get; set; }
    public DateOnly TicketCreationDate { get; set; }
    public bool ticketStatus { get; set; }
    public int AssignedTo { get; set; }
    public int? TicketDetailsID { get; set; }
}