namespace EntityFrameworkCorePartitionPoC.Infrastructure;

public class PartitionPocContext(DbContextOptions<PartitionPocContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        const string yearAndQuarter = "CAST(YEAR(OrderDate) AS nvarchar(4)) + 'Q' + CAST(DATEPART(QUARTER, OrderDate) AS nvarchar(1))";

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(customer => customer.Id).ValueGeneratedOnAdd();
            entity.Property(customer => customer.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(order => order.Id).ValueGeneratedOnAdd();
            entity.Property(order => order.PartitionKey).HasMaxLength(6).ValueGeneratedOnAddOrUpdate().HasComputedColumnSql(yearAndQuarter, stored: true);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.Property(item => item.Id).ValueGeneratedOnAdd();
            entity.Property(item => item.PartitionKey).HasMaxLength(6);
            entity.Property(item => item.Item).HasMaxLength(50);
            entity.Property(item => item.Price).HasPrecision(19, 4);
        });

        Seed(modelBuilder);
    }

    private static void Seed(ModelBuilder modelBuilder)
    {
        var adidas = new Customer { Id = Guid.Parse("ec79499a-3357-40b7-a5a6-d5483692a3fc"), Name = "Adidas" };
        var esteeLauder = new Customer { Id = Guid.Parse("6b7e5907-403f-468f-affe-ddf693d2ba69"), Name = "Estee Lauder" };
        var primark = new Customer { Id = Guid.Parse("dcd71abf-ad28-4cdc-bc0c-bcac0441b0f1"), Name = "Primark" };

        modelBuilder.Entity<Customer>().HasData(adidas, esteeLauder, primark);

        var order1 = new Order
        {
            Id = Guid.Parse("fca25cc3-fd41-4d57-bc3f-fab878b84e18"),
            CustomerId = adidas.Id,
            OrderDate = DateOnly.Parse("2024-02-05"),
            Volume = 42,
            Weight = 2342,
        };

        var order2 = new Order
        {
            Id = Guid.Parse("cd545d42-4722-47a2-a740-923ac2532c49"),
            CustomerId = esteeLauder.Id,
            OrderDate = DateOnly.Parse("2024-05-29"),
            Volume = 12,
            Weight = 2
        };

        var order3 = new Order
        {
            Id = Guid.Parse("23312ae0-3de3-4d39-ab9a-44e3c683c36c"),
            CustomerId = primark.Id,
            OrderDate = DateOnly.Parse("2024-08-01"),
            Volume = 4756,
            Weight = null
        };

        var order4 = new Order
        {
            Id = Guid.Parse("18917576-fb65-428b-a47b-e86cf79b7355"),
            CustomerId = primark.Id,
            OrderDate = DateOnly.Parse("2024-11-09"),
            Volume = 235,
            Weight = 288
        };

        var order5 = new Order
        {
            Id = Guid.Parse("bf231903-c26c-4878-8452-52909099683d"),
            CustomerId = primark.Id,
            OrderDate = DateOnly.Parse("2025-02-02"),
            Volume = null,
            Weight = 457847
        };

        modelBuilder.Entity<Order>().HasData(order1, order2, order3, order4, order5);

        ICollection<OrderItem> orderItems = [
            new() { Id = Guid.Parse("718d2717-e71a-42c8-9638-59c78c3a6f95"), PartitionKey = "2024Q1", OrderId = order1.Id, Item = "Item 1", Price = 238.12m },
            new() { Id = Guid.Parse("ba0a7a3f-0a6a-41fb-92df-7f77e3baaa65"), PartitionKey = "2024Q1", OrderId = order1.Id, Item = "Item 2", Price = 5776.43m },
            new() { Id = Guid.Parse("3d310b25-d3c2-48cd-b8cc-f926eecc9060"), PartitionKey = "2024Q2", OrderId = order2.Id, Item = "Item 1", Price = 238.12m },
            new() { Id = Guid.Parse("521e6807-ed49-4e3e-9323-a3fc16e653ab"), PartitionKey = "2024Q2", OrderId = order2.Id, Item = "Item 4", Price = 332.00m },
            new() { Id = Guid.Parse("d4d78920-0faa-438d-9bc3-73e9e5a5cbb2"), PartitionKey = "2024Q3", OrderId = order3.Id, Item = "Item 7", Price = 223.00m },
            new() { Id = Guid.Parse("3e756ab8-3b62-41a6-8a75-b6f8347f4f01"), PartitionKey = "2024Q3", OrderId = order3.Id, Item = "Item 9", Price = 988.00m },
            new() { Id = Guid.Parse("704cf48c-8a74-49ee-a283-0f8af2aa880a"), PartitionKey = "2024Q3", OrderId = order3.Id, Item = "Item 8", Price = 3.00m },
            new() { Id = Guid.Parse("454b3e7a-65ec-44a2-bc2f-2afc9fce2989"), PartitionKey = "2024Q3", OrderId = order3.Id, Item = "Item 4", Price = 88.00m },
            new() { Id = Guid.Parse("90879c03-1e43-486f-8f02-50ba4b9cc4a0"), PartitionKey = "2024Q4", OrderId = order4.Id, Item = "Item 7", Price = 223.00m },
            new() { Id = Guid.Parse("5546d99e-346b-4333-a716-7a30a562feac"), PartitionKey = "2024Q4", OrderId = order4.Id, Item = "Item 9", Price = 4576.00m },
            new() { Id = Guid.Parse("b5f7c577-89b4-41dd-814a-b4d290deb733"), PartitionKey = "2025Q1", OrderId = order5.Id, Item = "Item 5", Price = 89.25m },
            new() { Id = Guid.Parse("7fa197cb-950f-42e4-89bd-36ee23b8358a"), PartitionKey = "2025Q1", OrderId = order5.Id, Item = "Item 8", Price = 123.87m }
        ];

        modelBuilder.Entity<OrderItem>().HasData(orderItems);
    }
}