namespace EazyPay.Core.Entities;

public class Merchant : BaseEntity
{
    public string MerchantId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}