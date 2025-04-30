using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnergsoftInterview.Api.Migrations
{
    /// <inheritdoc />
    public partial class DataSourceType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DataSource",
                table: "Tenants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataSource",
                table: "Tenants");
        }
    }
}
