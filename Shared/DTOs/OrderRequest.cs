
namespace Shared.DTOs
{
    public record OrderRequest
    {
        public string BaskketId { get; init; }
        public ShippingAddressDto ShippingAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
