using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeskApi.Migrations
{
    /// <inheritdoc />
    public partial class DeskModelUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AvailabilityEndDate",
                table: "Desks",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailabilityEndDate",
                table: "Desks");
        }
    }
}
