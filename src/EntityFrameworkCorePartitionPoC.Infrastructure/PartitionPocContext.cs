using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class PartitionPocContext(DbContextOptions<PartitionPocContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(customer => customer.Name).IsUnique();
            entity.Property(customer => customer.Id).ValueGeneratedOnAdd().HasValueGenerator<SequentialGuidValueGenerator>();
            entity.Property(customer => customer.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(order => new { order.Id, order.PartitionKey });
            entity.Property(order => order.Id).ValueGeneratedOnAdd().HasValueGenerator<SequentialGuidValueGenerator>();
            entity.Property(order => order.PartitionKey).HasMaxLength(6).ValueGeneratedOnAdd().HasValueGenerator<OrderPartitionKeyGenerator>();
            entity.HasMany(order => order.OrderItems).WithOne(item => item.Order).HasForeignKey(item => new { item.OrderId, item.PartitionKey });
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(item => new { item.Id, item.PartitionKey });
            entity.Property(item => item.Id).ValueGeneratedOnAdd().HasValueGenerator<SequentialGuidValueGenerator>();
            entity.Property(item => item.PartitionKey).HasMaxLength(6);
            entity.Property(item => item.Item).HasMaxLength(50);
            entity.Property(item => item.Price).HasPrecision(19, 4);
        });
    }

    public class OrderPartitionKeyGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;

        public override string Next(EntityEntry entry)
        {
            var date = ((Order)entry.Entity).OrderDate;
            var quarter = Math.Ceiling(date.Month / 3.0);
            return $"{date.Year}Q{quarter}";
        }
    }
}