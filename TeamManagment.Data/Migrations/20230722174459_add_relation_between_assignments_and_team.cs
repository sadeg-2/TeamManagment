using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagment.Web.Data.Migrations
{
    public partial class add_relation_between_assignments_and_team : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "Assignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_TeamId",
                table: "Assignments",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_Teams_TeamId",
                table: "Assignments",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_Teams_TeamId",
                table: "Assignments");

            migrationBuilder.DropIndex(
                name: "IX_Assignments_TeamId",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Assignments");
        }
    }
}
