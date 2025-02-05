namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class Order : IPartitionedEntity
{
    public Guid ID { get; set; }

    public int PartitionKey { get; set; }

    public int PartitionGroupID { get; set; }

    public DateTime AggregateRootDate { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}