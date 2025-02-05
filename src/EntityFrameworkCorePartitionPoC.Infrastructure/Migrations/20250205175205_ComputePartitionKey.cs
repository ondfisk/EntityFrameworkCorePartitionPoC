using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntityFrameworkCorePartitionPoC.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ComputePartitionKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartitionKey",
                table: "Orders",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                computedColumnSql: "CAST(YEAR(OrderDate) AS nvarchar(4)) + 'Q' + CAST(DATEPART(QUARTER, OrderDate) AS nvarchar(1))",
                stored: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6);

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3d310b25-d3c2-48cd-b8cc-f926eecc9060"),
                column: "PartitionKey",
                value: "2024Q2");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3e756ab8-3b62-41a6-8a75-b6f8347f4f01"),
                column: "PartitionKey",
                value: "2024Q3");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("454b3e7a-65ec-44a2-bc2f-2afc9fce2989"),
                column: "PartitionKey",
                value: "2024Q3");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("521e6807-ed49-4e3e-9323-a3fc16e653ab"),
                column: "PartitionKey",
                value: "2024Q2");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("5546d99e-346b-4333-a716-7a30a562feac"),
                column: "PartitionKey",
                value: "2024Q4");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("704cf48c-8a74-49ee-a283-0f8af2aa880a"),
                column: "PartitionKey",
                value: "2024Q3");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("718d2717-e71a-42c8-9638-59c78c3a6f95"),
                column: "PartitionKey",
                value: "2024Q1");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("7fa197cb-950f-42e4-89bd-36ee23b8358a"),
                column: "PartitionKey",
                value: "2025Q1");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("90879c03-1e43-486f-8f02-50ba4b9cc4a0"),
                column: "PartitionKey",
                value: "2024Q4");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("b5f7c577-89b4-41dd-814a-b4d290deb733"),
                column: "PartitionKey",
                value: "2025Q1");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ba0a7a3f-0a6a-41fb-92df-7f77e3baaa65"),
                column: "PartitionKey",
                value: "2024Q1");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("d4d78920-0faa-438d-9bc3-73e9e5a5cbb2"),
                column: "PartitionKey",
                value: "2024Q3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartitionKey",
                table: "Orders",
                type: "nvarchar(6)",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(6)",
                oldMaxLength: 6,
                oldComputedColumnSql: "CAST(YEAR(OrderDate) AS nvarchar(4)) + 'Q' + CAST(DATEPART(QUARTER, OrderDate) AS nvarchar(1))");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3d310b25-d3c2-48cd-b8cc-f926eecc9060"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("3e756ab8-3b62-41a6-8a75-b6f8347f4f01"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("454b3e7a-65ec-44a2-bc2f-2afc9fce2989"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("521e6807-ed49-4e3e-9323-a3fc16e653ab"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("5546d99e-346b-4333-a716-7a30a562feac"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("704cf48c-8a74-49ee-a283-0f8af2aa880a"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("718d2717-e71a-42c8-9638-59c78c3a6f95"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("7fa197cb-950f-42e4-89bd-36ee23b8358a"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("90879c03-1e43-486f-8f02-50ba4b9cc4a0"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("b5f7c577-89b4-41dd-814a-b4d290deb733"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("ba0a7a3f-0a6a-41fb-92df-7f77e3baaa65"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "OrderItems",
                keyColumn: "Id",
                keyValue: new Guid("d4d78920-0faa-438d-9bc3-73e9e5a5cbb2"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("18917576-fb65-428b-a47b-e86cf79b7355"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("23312ae0-3de3-4d39-ab9a-44e3c683c36c"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("bf231903-c26c-4878-8452-52909099683d"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("cd545d42-4722-47a2-a740-923ac2532c49"),
                column: "PartitionKey",
                value: "");

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: new Guid("fca25cc3-fd41-4d57-bc3f-fab878b84e18"),
                column: "PartitionKey",
                value: "");
        }
    }
}
