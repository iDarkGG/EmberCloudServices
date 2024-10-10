using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

public partial class TicketDetail
{
    [Key]
    public int TicketDetailsID { get; set; }

    [StringLength(250)]
    public string? TicketMSG { get; set; }

    public DateOnly MsgDate { get; set; }

    public int? sentBy { get; set; }

    [InverseProperty("TicketDetails")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    [ForeignKey("sentBy")]
    [InverseProperty("TicketDetails")]
    public virtual Employee? sentByNavigation { get; set; }
}
