namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class PartitionPocContext(DbContextOptions<PartitionPocContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}