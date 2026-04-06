using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CivicConnect.Migrations
{
    /// <inheritdoc />
    public partial class UpdateComplaintModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedTo",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Complaints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Complaints",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Complaints");
        }
    }
}
