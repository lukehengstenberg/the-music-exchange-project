using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMusicExchangeProject.Migrations
{
    public partial class ChangedVar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MsgText",
                table: "Messages",
                newName: "message");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "message",
                table: "Messages",
                newName: "MsgText");
        }
    }
}
