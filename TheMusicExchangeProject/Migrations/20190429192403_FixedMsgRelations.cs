using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMusicExchangeProject.Migrations
{
    public partial class FixedMsgRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageGroupID",
                table: "UserGroups",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserGroups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MessageGroupID",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeSent",
                table: "Messages",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_MessageGroupID",
                table: "UserGroups",
                column: "MessageGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageGroupID",
                table: "Messages",
                column: "MessageGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_MessageGroups_MessageGroupID",
                table: "Messages",
                column: "MessageGroupID",
                principalTable: "MessageGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_MessageGroups_MessageGroupID",
                table: "UserGroups",
                column: "MessageGroupID",
                principalTable: "MessageGroups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_MessageGroups_MessageGroupID",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_MessageGroups_MessageGroupID",
                table: "UserGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UserGroups_AspNetUsers_UserId",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_MessageGroupID",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_UserGroups_UserId",
                table: "UserGroups");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageGroupID",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_UserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "MessageGroupID",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserGroups");

            migrationBuilder.DropColumn(
                name: "MessageGroupID",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "TimeSent",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Messages");
        }
    }
}
