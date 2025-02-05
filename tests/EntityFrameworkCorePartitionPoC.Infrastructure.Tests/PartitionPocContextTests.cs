
namespace EntityFrameworkCorePartitionPoC.Infrastructure.Tests;

public class PartitionPocContextTests
{
    private MsSqlContainer _database = null!;
    private PartitionPocContext _context = null!;

    [SetUp]
    public async Task SetUp()
    {
        _database = new MsSqlBuilder().WithImage("mcr.microsoft.com/mssql/server:latest").Build();

        await _database.StartAsync();

        var connectionString = _database.GetConnectionString();

        var orders = GenerateOrders();

        var builder = new DbContextOptionsBuilder<PartitionPocContext>()
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging()
            .UseSqlServer(connectionString)
            .UseSeeding((context, _) =>
            {
                context.Set<Order>().AddRange(orders);
                context.SaveChanges();
            })
            .UseAsyncSeeding(async (context, _, cancellationToken) =>
            {
                context.Set<Order>().AddRange(orders);
                await context.SaveChangesAsync(cancellationToken);
            });

        var context = new PartitionPocContext(builder.Options);

        await context.Database.MigrateAsync();

        _context = context;
    }

    [Test]
    public async Task AfterMigrationDatabaseContainsCustomers()
    {
        var customers = await _context.Customers.ToListAsync();

        Assert.That(customers, Has.Count.EqualTo(3));
    }

    [Test]
    public async Task AfterMigrationDatabaseContainsOrders()
    {
        var orders = await _context.Orders.ToListAsync();

        Assert.That(orders, Has.Count.EqualTo(5));
    }

    [Test]
    public async Task AfterMigrationDatabaseContainsOrderItems()
    {
        var orderItems = await _context.OrderItems.ToListAsync();

        Assert.That(orderItems, Has.Count.EqualTo(12));
    }

    [Test]
    public async Task CreateOrderCreatesOrderAndOrderItems()
    {
        var customer = await _context.Customers.FirstAsync(c => c.Name == "Estée Lauder");

        Order order = new()
        {
            OrderDate = DateOnly.Parse("2024-05-23"),
            Customer = customer,
            Volume = 12,
            Weight = 4225,
            OrderItems = [
                new() { Item = "Boxes", Price = 43.00m },
                new() { Item = "Hot Air", Price = 0.00m }
            ]
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(order.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(order.PartitionKey, Is.EqualTo("2024Q2"));
            Assert.That(await _context.OrderItems.Where(o => o.OrderId == order.Id).ToListAsync(), Has.Count.EqualTo(2));
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
        await _database.DisposeAsync();
    }

    private ICollection<Order> GenerateOrders()
    {
        var adidas = new Customer { Name = "Adidas" };
        var esteeLauder = new Customer { Name = "Estée Lauder" };
        var primark = new Customer { Name = "Primark" };

        var order1 = new Order
        {
            Id = Guid.Parse("fca25cc3-fd41-4d57-bc3f-fab878b84e18"),
            Customer = adidas,
            OrderDate = DateOnly.Parse("2024-02-05"),
            Volume = 42,
            Weight = 2342,
            OrderItems = [
                new() { Id = Guid.Parse("718d2717-e71a-42c8-9638-59c78c3a6f95"), Item = "Item 1", Price = 238.12m },
                new() { Id = Guid.Parse("ba0a7a3f-0a6a-41fb-92df-7f77e3baaa65"), Item = "Item 2", Price = 5776.43m }
            ]
        };

        var order2 = new Order
        {
            Id = Guid.Parse("cd545d42-4722-47a2-a740-923ac2532c49"),
            Customer = esteeLauder,
            OrderDate = DateOnly.Parse("2024-05-29"),
            Volume = 12,
            Weight = 2,
            OrderItems = [
                new() { Id = Guid.Parse("3d310b25-d3c2-48cd-b8cc-f926eecc9060"), Item = "Item 1", Price = 238.12m },
                new() { Id = Guid.Parse("521e6807-ed49-4e3e-9323-a3fc16e653ab"), Item = "Item 4", Price = 332.00m }
            ]
        };

        var order3 = new Order
        {
            Id = Guid.Parse("23312ae0-3de3-4d39-ab9a-44e3c683c36c"),
            Customer = primark,
            OrderDate = DateOnly.Parse("2024-08-01"),
            Volume = 4756,
            Weight = null,
            OrderItems = [
                new() { Id = Guid.Parse("d4d78920-0faa-438d-9bc3-73e9e5a5cbb2"), Item = "Item 7", Price = 223.00m },
                new() { Id = Guid.Parse("3e756ab8-3b62-41a6-8a75-b6f8347f4f01"), Item = "Item 9", Price = 988.00m },
                new() { Id = Guid.Parse("704cf48c-8a74-49ee-a283-0f8af2aa880a"), Item = "Item 8", Price = 3.00m },
                new() { Id = Guid.Parse("454b3e7a-65ec-44a2-bc2f-2afc9fce2989"), Item = "Item 4", Price = 88.00m }
            ]
        };

        var order4 = new Order
        {
            Id = Guid.Parse("18917576-fb65-428b-a47b-e86cf79b7355"),
            Customer = primark,
            OrderDate = DateOnly.Parse("2024-11-09"),
            Volume = 235,
            Weight = 288,
            OrderItems = [
                new() { Id = Guid.Parse("90879c03-1e43-486f-8f02-50ba4b9cc4a0"), Item = "Item 7", Price = 223.00m },
                new() { Id = Guid.Parse("5546d99e-346b-4333-a716-7a30a562feac"), Item = "Item 9", Price = 4576.00m }
            ]
        };

        var order5 = new Order
        {
            Id = Guid.Parse("bf231903-c26c-4878-8452-52909099683d"),
            Customer = primark,
            OrderDate = DateOnly.Parse("2025-02-02"),
            Volume = null,
            Weight = 457847,
            OrderItems = [
                new() { Id = Guid.Parse("b5f7c577-89b4-41dd-814a-b4d290deb733"), Item = "Item 5", Price = 89.25m },
                new() { Id = Guid.Parse("7fa197cb-950f-42e4-89bd-36ee23b8358a"), Item = "Item 8", Price = 123.87m }
            ]
        };

        return [order1, order2, order3, order4, order5];
    }
}