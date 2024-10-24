
using EmberAPI.Models;

namespace EmberAPI.Dtos;

public class InstanceDto
{
    public string InstanceID { get; set; }
    public int? ClientId { get; set; }
    public string? DatacenterID { get; set; }
    public string? InstanceName { get; set; }
    public DateOnly CreationDate { get; set; }
}