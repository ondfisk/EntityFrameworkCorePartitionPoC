using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCorePartitionPoC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PartitionOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PARTITION FUNCTION [PartitionByQuarter] (nvarchar(6))
                    AS RANGE FOR VALUES (
                        '2020Q1', '2020Q2', '2020Q3', '2020Q4',
                        '2021Q1', '2021Q2', '2021Q3', '2021Q4',
                        '2022Q1', '2022Q2', '2022Q3', '2022Q4',
                        '2023Q1', '2023Q2', '2023Q3', '2023Q4',
                        '2024Q1', '2024Q2', '2024Q3', '2024Q4',
                        '2025Q1', '2025Q2', '2025Q3', '2025Q4',
                        '2026Q1', '2026Q2', '2026Q3', '2026Q4',
                        '2027Q1', '2027Q2', '2027Q3', '2027Q4'
                    )
                GO

                CREATE PARTITION SCHEME [PartitionByQuarter]
                    AS PARTITION [PartitionByQuarter]
                    ALL TO ('PRIMARY')
                GO

                CREATE CLUSTERED INDEX IX_Orders_PartitionKey ON dbo.Orders (PartitionKey)
                    WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                            ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
                    ON PartitionByQuarter(PartitionKey)
                GO

                CREATE CLUSTERED INDEX IX_OrderItems_PartitionKey ON dbo.OrderItems (PartitionKey)
                    WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
                            ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
                    ON PartitionByQuarter(PartitionKey)
                GO
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                DROP INDEX IX_OrderItems_PartitionKey ON dbo.OrdersItems
                GO

                DROP INDEX IX_Order_PartitionKey ON dbo.Orders
                GO

                DROP PARTITION SCHEME [PartitionByQuarter]
                GO

                DROP PARTITION FUNCTION [PartitionByQuarter]
                GO
            ");
        }
    }
}
