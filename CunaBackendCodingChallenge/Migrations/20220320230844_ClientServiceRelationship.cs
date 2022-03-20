using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CunaBackendCodingChallenge.Migrations
{
    public partial class ClientServiceRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceReports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientRequestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceReports_ClientRequests_ClientRequestId",
                        column: x => x.ClientRequestId,
                        principalTable: "ClientRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReports_ClientRequestId",
                table: "ServiceReports",
                column: "ClientRequestId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceReports");
        }
    }
}
