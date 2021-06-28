using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckoutSys.Infrastructure.Migrations.ApplicationDb
{
    public partial class AddProductCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCategories", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ProductCategories",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn", "Name", "ParentCategoryId" },
                values: new object[] { 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Fruits", 0 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "CreatedBy", "CreatedOn", "Description", "Image", "IsDeleted", "IsPublished", "LastModifiedBy", "LastModifiedOn", "Name", "NotReturnable", "OrderMaximumQuantity", "OrderMinimumQuantity", "ProductCategoryId", "Rate", "StockQuantity" },
                values: new object[] { 2, "barcode", "ck", new DateTime(2021, 6, 27, 16, 52, 28, 486, DateTimeKind.Utc).AddTicks(8943), "Orange", null, false, true, null, null, "Orange", true, 200, 0, 0, 0.25m, 200 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Barcode", "CreatedBy", "CreatedOn", "Description", "Image", "IsDeleted", "IsPublished", "LastModifiedBy", "LastModifiedOn", "Name", "NotReturnable", "OrderMaximumQuantity", "OrderMinimumQuantity", "ProductCategoryId", "Rate", "StockQuantity" },
                values: new object[] { 1, "barcode", "ck", new DateTime(2021, 6, 27, 16, 52, 28, 485, DateTimeKind.Utc).AddTicks(3953), "Apple", null, false, true, null, null, "Apple", true, 200, 0, 0, 0.6m, 200 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductCategories");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
