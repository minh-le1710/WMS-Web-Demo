using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wms.Web.Migrations
{
    /// <inheritdoc />
    public partial class AddContainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ContainerNo = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Size = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Line = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    CargoStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    InDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomsStatus = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ContainerNo",
                table: "Containers",
                column: "ContainerNo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");
        }
    }
}
