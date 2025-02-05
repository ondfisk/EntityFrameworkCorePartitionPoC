namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public interface IPartitionedEntity
{
    Guid Id { get; set; }
    string PartitionKey { get; set; }
}