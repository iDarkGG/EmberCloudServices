namespace EmberAPI.Dtos;

public class FacturaDetalleDto
{
    public int FacturaDetalleID { get; set; }
    public string DetalleF { get; set; }
    public string PeriodoFacturado { get; set; }
    public decimal MontoFacturado { get; set; }
}