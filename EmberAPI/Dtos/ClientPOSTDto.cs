namespace EmberAPI.Dtos;

public class ClientPOSTDto
{
    public string ClientName { get; set; } = null!;
    public string ClientContactNumber { get; set; } = null!;
    public string ClientEmail { get; set; } = null!;
    public bool? Status { get; set; }
}