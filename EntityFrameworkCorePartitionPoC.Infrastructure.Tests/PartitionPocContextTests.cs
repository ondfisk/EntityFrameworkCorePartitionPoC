namespace EntityFrameworkCorePartitionPoC.Infrastructure.Tests;

public class PartitionPocContextTests
{
    private PartitionPocContext _db = null!;

    [SetUp]
    public void Setup()
    {
        //var config = new ConfigurationBuilder().AddUserSecrets<Tests>().Build();
        //var builder = new DbContextOptionsBuilder<EdcDb>().UseSqlServer(config.GetConnectionString("EdcDb"));
        //var db = new PartitionPocContext(builder.Options);
        //await db.Database.MigrateAsync();
        //_db = db;
    }

    [Test]
    public void AddOrderWithItems()
    {
        var date = DateTime.UtcNow.Date;
        var order = new Order
        {
            AggregateRootDate = date,
            OrderItems =
            [
                new() { AggregateRootDate = date },
                new() { AggregateRootDate = date },
            ]
        };

        _db.Orders.Add(order);
        _db.SaveChanges();
    }

    [Test]
    public async Task AddOrder()
    {
        var order = new Order { AggregateRootDate = DateTime.Today, PartitionGroupID = 3 };

        _db.Orders.Add(order);
        await _db.SaveChangesAsync();

        Assert.Multiple(() =>
        {
            Assert.That(order.ID, Is.Not.EqualTo(Guid.NewGuid()));
            Assert.That(order.PartitionKey, Is.Not.EqualTo(0));
        });
    }

    [Test]
    public async Task UpdateOrder()
    {
        var order = new Order { AggregateRootDate = DateTime.Today, PartitionGroupID = 2 };

        _db.Orders.Add(order);

        await _db.SaveChangesAsync();

        Assert.Multiple(() =>
        {
            Assert.That(order.ID, Is.Not.EqualTo(Guid.NewGuid()));
            Assert.That(order.PartitionKey, Is.Not.EqualTo(0));
        });
    }
}