
namespace EmberAPI.Dtos;

public class FacturaDto
{
    public int FacturaID { get; set; }
    public DateTime FacturaDate { get; set; }
    public decimal FacturaMonto { get; set; }
    public decimal ClientID { get; set; }
}