using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Keyless]
[Table("PaymentInfo")]
public partial class PaymentInfo
{
    public int? ClientID { get; set; }

    [StringLength(100)]
    public string PaymentData { get; set; } = null!;

    public DateOnly PaymentExpireDate { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string paymentPIN { get; set; } = null!;

    [ForeignKey("ClientID")]
    public virtual Client? Client { get; set; }
}
