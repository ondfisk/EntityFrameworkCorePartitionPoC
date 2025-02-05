namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class Customer
{
    private ICollection<Order>? _orders;

    public Guid Id { get; set; }

    public required string Name { get; set; }

    public ICollection<Order> Orders
    {
        get => _orders ?? throw new InvalidOperationException("Uninitialized property: " + nameof(Orders));
        set => _orders = value;
    }
}