﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class AddFirstnameLastnameToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lastname",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lastname",
                table: "AspNetUsers");
        }
    }
}
