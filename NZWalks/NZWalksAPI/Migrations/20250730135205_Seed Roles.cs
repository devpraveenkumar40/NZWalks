using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalksAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9fadac8-e2ad-49d6-8193-701a07dbe3cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c25efd11-714e-4282-a46d-e3c0733f655a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "38db9e59-aa95-491a-a840-5053f62f7d2b", "30-07-2025", "Admin", "ADMIN" },
                    { "bfbb9656-f3c8-4dec-849f-ed33a4cbc892", "30-07-2025", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38db9e59-aa95-491a-a840-5053f62f7d2b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfbb9656-f3c8-4dec-849f-ed33a4cbc892");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9fadac8-e2ad-49d6-8193-701a07dbe3cf", null, "admin", "ADMIN" },
                    { "c25efd11-714e-4282-a46d-e3c0733f655a", null, "user", "USER" }
                });
        }
    }
}
