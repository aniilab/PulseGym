using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseGym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class WorkoutDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "WorkoutRequests",
                newName: "WorkoutDateTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkoutDateTime",
                table: "WorkoutRequests",
                newName: "DateTime");

        }
    }
}
