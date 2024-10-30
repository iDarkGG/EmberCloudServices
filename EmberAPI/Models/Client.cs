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

    [StringLength(15)]
    [Unicode(false)]
    public string? ClientContactNumber { get; set; }

    [StringLength(55)]
    [Unicode(false)]
    public string? ClientEmail { get; set; }

    public bool? Status { get; set; }

    public DateOnly CreationDate { get; set; }

    [InverseProperty("Client")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    [InverseProperty("Client")]
    public virtual ICollection<Instance> Instances { get; set; } = new List<Instance>();

    [InverseProperty("Client")]
    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
}
