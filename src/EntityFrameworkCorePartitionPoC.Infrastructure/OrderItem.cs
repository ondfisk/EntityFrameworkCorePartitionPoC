namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class OrderItem : IPartitionedEntity
{
    private Order? _order;

    public Guid Id { get; set; }

    public string PartitionKey { get; set; } = string.Empty;

    public Guid OrderId { get; set; }

    public Order Order
    {
        get => _order ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Order));
        set => _order = value;
    }

    public required string Item { get; set; }

    public decimal Price { get; set; }
}