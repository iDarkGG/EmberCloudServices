using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Ticket")]
public partial class Ticket
{
    [Key]
    public int ticketID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? ticketTitle { get; set; }

    public DateOnly ticketCreationDate { get; set; }

    public bool ticketStatus { get; set; }

    public int? AssignedTo { get; set; }

    public int? TicketDetailsID { get; set; }

    [ForeignKey("AssignedTo")]
    [InverseProperty("Tickets")]
    public virtual Employee? AssignedToNavigation { get; set; }

    [ForeignKey("TicketDetailsID")]
    [InverseProperty("Tickets")]
    public virtual TicketDetail? TicketDetails { get; set; }
}
