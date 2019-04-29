using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMusicExchangeProject.Migrations
{
    public partial class Connections : Migration
    {
        /**
         * 
         * Applies the Migration.
         * 
         **/
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestFromID = table.Column<string>(nullable: true),
                    RequestToId = table.Column<string>(nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Connections_AspNetUsers_RequestToId",
                        column: x => x.RequestToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_RequestToId",
                table: "Connections",
                column: "RequestToId");
        }
        /**
         * 
         * Removes the Migration.
         * 
         */
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");
        }
    }
}
