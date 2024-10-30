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

    public int? EmployeeID { get; set; }

    public int? ClientID { get; set; }

    public int TicketID { get; set; }

    [ForeignKey("ClientID")]
    [InverseProperty("TicketDetails")]
    public virtual Client? Client { get; set; }

    [ForeignKey("EmployeeID")]
    [InverseProperty("TicketDetails")]
    public virtual Employee? Employee { get; set; }

    [ForeignKey("TicketID")]
    [InverseProperty("TicketDetails")]
    public virtual Ticket Ticket { get; set; } = null!;
}
