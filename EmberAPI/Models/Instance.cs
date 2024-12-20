﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Instance")]
[Index("InstanceName", Name = "UQ__Instance__132AFCD8651AA162", IsUnique = true)]
public partial class Instance
{
    [Key]
    public int InstanceID { get; set; }

    public int? ClientID { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string? DataCenterID { get; set; }

    [StringLength(50)]
    public string? InstanceName { get; set; }

    public DateOnly CreationDate { get; set; }

    [ForeignKey("ClientID")]
    [InverseProperty("Instances")]
    public virtual Client? Client { get; set; }

    [InverseProperty("Instance")]
    public virtual ICollection<CreatedUser> CreatedUsers { get; set; } = new List<CreatedUser>();

    [ForeignKey("DataCenterID")]
    [InverseProperty("Instances")]
    public virtual DataCenter? DataCenter { get; set; }
}
