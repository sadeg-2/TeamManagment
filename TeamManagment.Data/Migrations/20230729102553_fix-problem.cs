﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagment.Web.Data.Migrations
{
    public partial class fixproblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewrId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewrId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewrId",
                table: "Reviews",
                column: "ReviewrId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewrId",
                table: "Reviews");

            migrationBuilder.AlterColumn<string>(
                name: "ReviewrId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ReviewrId",
                table: "Reviews",
                column: "ReviewrId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
