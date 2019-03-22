using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMusicExchangeProject.Migrations
{
    public partial class AddBlocks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RequestFromID",
                table: "Connections",
                newName: "RequestFromId");

            migrationBuilder.AlterColumn<string>(
                name: "RequestFromId",
                table: "Connections",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Blocks",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BlockFromId = table.Column<string>(nullable: true),
                    BlockToId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_BlockFromId",
                        column: x => x.BlockFromId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Blocks_AspNetUsers_BlockToId",
                        column: x => x.BlockToId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_RequestFromId",
                table: "Connections",
                column: "RequestFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockFromId",
                table: "Blocks",
                column: "BlockFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_BlockToId",
                table: "Blocks",
                column: "BlockToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Connections_AspNetUsers_RequestFromId",
                table: "Connections",
                column: "RequestFromId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Connections_AspNetUsers_RequestFromId",
                table: "Connections");

            migrationBuilder.DropTable(
                name: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Connections_RequestFromId",
                table: "Connections");

            migrationBuilder.RenameColumn(
                name: "RequestFromId",
                table: "Connections",
                newName: "RequestFromID");

            migrationBuilder.AlterColumn<string>(
                name: "RequestFromID",
                table: "Connections",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
