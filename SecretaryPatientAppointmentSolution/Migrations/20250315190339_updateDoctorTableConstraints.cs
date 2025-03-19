using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretaryPatientAppointmentSolution.Migrations
{
    /// <inheritdoc />
    public partial class updateDoctorTableConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DoctorId",
                table: "Schedules",
                column: "DoctorId",
                unique: true);
        }
    }
}
