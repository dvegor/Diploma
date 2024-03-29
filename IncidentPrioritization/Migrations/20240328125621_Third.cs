using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IncidentPrioritization.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Incidents");

            migrationBuilder.AddColumn<int>(
                name: "StatusCode",
                table: "Incidents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusCode",
                table: "Incidents");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Incidents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
