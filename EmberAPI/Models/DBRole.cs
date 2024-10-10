using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

public partial class DBRole
{
    [Key]
    public int DBRolesID { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [InverseProperty("DBRoles")]
    public virtual ICollection<CreatedUser> CreatedUsers { get; set; } = new List<CreatedUser>();
}
