using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("Factura")]
public partial class Factura
{
    [Key]
    public int FacturaID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime FacturaDate { get; set; }

    [Column(TypeName = "money")]
    public decimal FacturaMonto { get; set; }

    public int? FacturaDetalleID { get; set; }

    [ForeignKey("FacturaDetalleID")]
    [InverseProperty("Facturas")]
    public virtual FacturaDetalle? FacturaDetalle { get; set; }
}
