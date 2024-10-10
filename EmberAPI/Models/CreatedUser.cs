using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Index("userNameHash", Name = "UQ__CreatedU__1F25CA80BD2CD418", IsUnique = true)]
public partial class CreatedUser
{
    [Key]
    public int CreatedUsersID { get; set; }

    [StringLength(150)]
    public string userNameHash { get; set; } = null!;

    [StringLength(200)]
    public string userPasswordHash { get; set; } = null!;

    public int? DBRolesID { get; set; }

    [ForeignKey("DBRolesID")]
    [InverseProperty("CreatedUsers")]
    public virtual DBRole? DBRoles { get; set; }
}
