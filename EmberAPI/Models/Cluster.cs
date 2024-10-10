using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Cluster")]
public partial class Cluster
{
    [Key]
    public int ClusterID { get; set; }

    public int? ClientID { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? DataCenterID { get; set; }

    [StringLength(50)]
    public string? ClusterName { get; set; }

    public DateOnly CreationDate { get; set; }

    [ForeignKey("ClientID")]
    [InverseProperty("Clusters")]
    public virtual Client? Client { get; set; }

    [ForeignKey("DataCenterID")]
    [InverseProperty("Clusters")]
    public virtual DataCenter? DataCenter { get; set; }
}
