namespace EmberAPI.Dtos;

public class DataCenterDto
{
    public string DatacenterId { get; set; }
    public string? DatacenterName { get; set; }
    public int Capacity { get; set; }
    public string? Location { get; set; }
}