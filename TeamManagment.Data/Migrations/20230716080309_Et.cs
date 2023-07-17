﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamManagment.Web.Data.Migrations
{
    public partial class Et : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamLeaderUserName2",
                table: "Teams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamLeaderUserName2",
                table: "Teams");
        }
    }
}
