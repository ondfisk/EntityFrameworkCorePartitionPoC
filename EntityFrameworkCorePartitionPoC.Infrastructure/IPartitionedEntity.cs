namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public interface IPartitionedEntity
{
    Guid ID { get; set; }
    int PartitionKey { get; set; }
    int PartitionGroupID { get; set; }
    DateTime AggregateRootDate { get; set; }
}