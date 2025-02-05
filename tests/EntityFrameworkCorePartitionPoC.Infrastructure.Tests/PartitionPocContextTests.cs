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

        var options = new DbContextOptionsBuilder<PartitionPocContext>().UseSqlServer(connectionString).Options;

        var context = new PartitionPocContext(options);

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
        Order order = new()
        {
            OrderDate = DateOnly.FromDateTime(DateTime.Today),
            CustomerId = Guid.Parse("6b7e5907-403f-468f-affe-ddf693d2ba69"),
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
            Assert.That(await _context.OrderItems.Where(o => o.OrderId == order.Id).ToListAsync(), Has.Count.EqualTo(2));
        }
    }

    [TearDown]
    public async Task TearDown()
    {
        await _context.DisposeAsync();
        await _database.DisposeAsync();
    }
}