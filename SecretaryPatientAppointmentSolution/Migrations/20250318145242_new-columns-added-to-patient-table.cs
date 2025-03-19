using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretaryPatientAppointmentSolution.Migrations
{
    /// <inheritdoc />
    public partial class newcolumnsaddedtopatienttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegDate",
                table: "Patients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RegDate",
                table: "Patients");
        }
    }
}
