namespace EmberAPI.Dtos;

public class ClientDto
{
    public int ClientID { get; set; }
    public string ClientName { get; set; } = null!;
    public string ClientContactNumber { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public bool? Status { get; set; }
    public DateOnly CreationDate { get; set; }
}