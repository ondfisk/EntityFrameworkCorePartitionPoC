namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class Order : IPartitionedEntity
{
    private Customer? _customer;

    private ICollection<OrderItem>? _orderItems;

    public Guid Id { get; set; }

    public string PartitionKey { get; set; } = null!;

    public required DateOnly OrderDate { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer
    {
        get => _customer ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Customer));
        set => _customer = value;
    }

    public double? Volume { get; set; }

    public double? Weight { get; set; }

    public ICollection<OrderItem> OrderItems
    {
        get => _orderItems ?? throw new InvalidOperationException("Uninitialized property: " + nameof(OrderItems));
        set => _orderItems = value;
    }
}