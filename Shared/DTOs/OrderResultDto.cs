
namespace Shared.DTOs
{
    public record OrderResultDto
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }
        public ShippingAddressDto ShippingAddress { get; init; }
        public ICollection<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
        public string PaymentStatus { get; init; }
        public string DeliveryMethods { get; init; }
        public decimal Subtotal { get; init; }
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; init; } = string.Empty;
        public decimal Total { get; set; }
    }
}
