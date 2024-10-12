using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Employee")]
public partial class  Employee
{
    [Key]
    public int EmployeeID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string EmployeeName { get; set; } = null!;

    [StringLength(25)]
    [Unicode(false)]
    public string? EmployeeRole { get; set; }

    [InverseProperty("sentByNavigation")]
    public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();

    [InverseProperty("AssignedToNavigation")]
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
