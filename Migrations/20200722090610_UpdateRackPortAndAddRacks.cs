using Microsoft.EntityFrameworkCore.Migrations;

namespace myITOffice.Migrations
{
    public partial class UpdateRackPortAndAddRacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RackId",
                table: "RackPorts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RackId",
                table: "RackPorts");
        }
    }
}
