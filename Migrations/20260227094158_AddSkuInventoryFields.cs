using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddSkuInventoryFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Skus",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Group",
                table: "Skus",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxStock",
                table: "Skus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinStock",
                table: "Skus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReorderPoint",
                table: "Skus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "Group",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "MaxStock",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "MinStock",
                table: "Skus");

            migrationBuilder.DropColumn(
                name: "ReorderPoint",
                table: "Skus");
        }
    }
}
