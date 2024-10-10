using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Client")]
public partial class Client
{
    [Key]
    public int ClientID { get; set; }

    [StringLength(50)]
    public string ClientName { get; set; } = null!;

    public bool? Status { get; set; }

    public DateOnly CreationDate { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Cluster> Clusters { get; set; } = new List<Cluster>();
}
