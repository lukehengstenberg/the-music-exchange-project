using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheMusicExchangeProject.Migrations
{
    public partial class AddSkills : Migration
    {
        /**
         * 
         * Applies the Migration.
         * 
         **/
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillLevel",
                columns: table => new
                {
                    LevelID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillLevel", x => x.LevelID);
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillName = table.Column<string>(nullable: false),
                    UserID = table.Column<string>(nullable: true),
                    LevelID = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillName);
                    table.ForeignKey(
                        name: "FK_Skill_SkillLevel_LevelID",
                        column: x => x.LevelID,
                        principalTable: "SkillLevel",
                        principalColumn: "LevelID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Skill_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Skill_LevelID",
                table: "Skill",
                column: "LevelID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_UserID",
                table: "Skill",
                column: "UserID");
        }

        /**
         * 
         * Removes the Migration.
         * 
         */
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "SkillLevel");
        }
    }
}
