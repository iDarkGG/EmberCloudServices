
using EmberAPI.Models;

namespace EmberAPI.Dtos;

public class ClusterDto
{
    public int ClusterId { get; set; }
    public int? ClientId { get; set; }
    public string? DatacenterID { get; set; }
    public string? ClusterName { get; set; }
    public DateOnly CreationDate { get; set; }
    public virtual DataCenter? DataCenter { get; set; }
}