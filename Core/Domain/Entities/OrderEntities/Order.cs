namespace Domain.Entities.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; }
        public Entities.OrderEntities.Address ShippingAddress { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethods DeliveryMethods { get; set; }
        public int? DeliveryMethodsId { get; set; }
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
