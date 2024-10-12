namespace EmberAPI.Dtos;

public class PaymentInfoDto
{
    public int ClientID { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime PaymentExpireDate { get; set; }
    public string PaymentPIN { get; set; }
}