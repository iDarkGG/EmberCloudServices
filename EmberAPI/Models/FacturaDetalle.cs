using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmberAPI.Models;

[Table("FacturaDetalle")]
public partial class FacturaDetalle
{
    [Key]
    public int FacturaDetalleID { get; set; }

    [StringLength(100)]
    public string DetalleF { get; set; } = null!;

    [StringLength(100)]
    public string PeriodoFacturado { get; set; } = null!;

    [Column(TypeName = "money")]
    public decimal MontoFacturado { get; set; }

    [InverseProperty("FacturaDetalle")]
    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
