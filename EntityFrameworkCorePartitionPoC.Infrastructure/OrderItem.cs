namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class OrderItem : IPartitionedEntity
{
    public Guid ID { get; set; }

    public int PartitionKey { get; set; }

    public DateTime AggregateRootDate { get; set; }

    public Order? Order { get; set; }

    public Guid OrderID { get; set; }

    public int PartitionGroupID { get; set; }
}