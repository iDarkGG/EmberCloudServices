using EmberAPI.Models;

namespace EmberAPI.Dtos;

public class EmployeeDto
{
    public int EmployeeID { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string? EmployeeRole { get; set; }
    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    
} 