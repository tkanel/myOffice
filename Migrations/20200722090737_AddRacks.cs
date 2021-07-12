using Microsoft.EntityFrameworkCore.Migrations;

namespace myITOffice.Migrations
{
    public partial class AddRacks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Racks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(nullable: true),
                    AssetNr = table.Column<int>(nullable: false),
                    PortsNr = table.Column<int>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RackPorts_RackId",
                table: "RackPorts",
                column: "RackId");

            migrationBuilder.AddForeignKey(
                name: "FK_RackPorts_Racks_RackId",
                table: "RackPorts",
                column: "RackId",
                principalTable: "Racks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RackPorts_Racks_RackId",
                table: "RackPorts");

            migrationBuilder.DropTable(
                name: "Racks");

            migrationBuilder.DropIndex(
                name: "IX_RackPorts_RackId",
                table: "RackPorts");
        }
    }
}
