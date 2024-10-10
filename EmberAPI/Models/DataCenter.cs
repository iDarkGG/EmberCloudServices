using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("DataCenter")]
public partial class DataCenter
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string DataCenterID { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? DataCenterName { get; set; }

    public int Capacity { get; set; }

    [StringLength(100)]
    public string? Location { get; set; }

    [InverseProperty("DataCenter")]
    public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
}
