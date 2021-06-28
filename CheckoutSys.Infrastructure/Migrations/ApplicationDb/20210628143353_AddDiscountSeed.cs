using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CheckoutSys.Infrastructure.Migrations.ApplicationDb
{
    public partial class AddDiscountSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "BundleSelectionOperation", "BundleSelectionProductId", "BundleSelectionQuantity", "CreatedBy", "CreatedOn", "DiscountAmount", "DiscountLimitation", "DiscountLimitationId", "DiscountPercentage", "DiscountType", "DiscountTypeId", "EndDateUtc", "EntityId", "IsCumulative", "IsProductBundleEnabled", "LastModifiedBy", "LastModifiedOn", "LimitationTimes", "MaximumDiscountAmount", "MaximumDiscountedQuantity", "Name", "StartDateUtc", "UsePercentage" },
                values: new object[,]
                {
                    { 1, "MultipleOf", 1, 2, "ck", new DateTime(2021, 6, 28, 14, 33, 52, 345, DateTimeKind.Utc).AddTicks(2163), 0m, 0, 0, 100m, 2, 2, new DateTime(2021, 7, 28, 14, 33, 52, 346, DateTimeKind.Utc).AddTicks(1328), 1, true, true, null, null, 0, 10000m, null, "buy 1 get 1 free", new DateTime(2021, 6, 28, 14, 33, 52, 345, DateTimeKind.Utc).AddTicks(9062), true },
                    { 2, "MultipleOf", 2, 3, "ck", new DateTime(2021, 6, 28, 14, 33, 52, 347, DateTimeKind.Utc).AddTicks(2479), 0m, 0, 0, 100m, 2, 2, new DateTime(2021, 7, 28, 14, 33, 52, 347, DateTimeKind.Utc).AddTicks(2505), 2, true, true, null, null, 0, 10000m, null, "3 for the price of 2 on Oranges", new DateTime(2021, 6, 28, 14, 33, 52, 347, DateTimeKind.Utc).AddTicks(2504), true }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 6, 28, 14, 33, 52, 343, DateTimeKind.Utc).AddTicks(9799));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 6, 28, 14, 33, 52, 344, DateTimeKind.Utc).AddTicks(8142));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2021, 6, 28, 10, 52, 29, 596, DateTimeKind.Utc).AddTicks(6484));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2021, 6, 28, 10, 52, 29, 600, DateTimeKind.Utc).AddTicks(1403));
        }
    }
}
