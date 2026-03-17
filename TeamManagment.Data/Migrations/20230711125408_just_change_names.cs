using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagment.Web.Data.Migrations
{
    public partial class just_change_names : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TeamMembers_AssigneeId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatorId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Tasks",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_CreatorId",
                table: "Tasks",
                newName: "IX_Tasks_AssigneeId");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Assignments",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_AssigneeId",
                table: "Assignments",
                newName: "IX_Assignments_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TeamMembers_CreatorId",
                table: "Assignments",
                column: "CreatorId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssigneeId",
                table: "Tasks",
                column: "AssigneeId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assignments_TeamMembers_CreatorId",
                table: "Assignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssigneeId",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "AssigneeId",
                table: "Tasks",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                newName: "IX_Tasks_CreatorId");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Assignments",
                newName: "AssigneeId");

            migrationBuilder.RenameIndex(
                name: "IX_Assignments_CreatorId",
                table: "Assignments",
                newName: "IX_Assignments_AssigneeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assignments_TeamMembers_AssigneeId",
                table: "Assignments",
                column: "AssigneeId",
                principalTable: "TeamMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatorId",
                table: "Tasks",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
