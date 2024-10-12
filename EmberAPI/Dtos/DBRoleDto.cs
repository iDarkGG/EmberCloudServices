using EmberAPI.Models;

namespace EmberAPI.Dtos;

public class DBRoleDto
{
    public  int DBRolesID { get; set; }
    public  string RoleName { get; set; } = null!;
    public virtual ICollection<CreatedUser> CreatedUsers { get; set; } = new HashSet<CreatedUser>();
    
}