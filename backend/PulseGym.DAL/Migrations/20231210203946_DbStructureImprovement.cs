using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseGym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DbStructureImprovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Trainers_TrainerId",
                table: "Activities");

            migrationBuilder.DropForeignKey(
                name: "FK_Clients_MembershipPrograms_MembershipProgramId",
                table: "Clients");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_Clients_ClientId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "ActivityClient");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_ClientId",
                table: "Workouts");

            migrationBuilder.DropIndex(
                name: "IX_Clients_MembershipProgramId",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Activities_TrainerId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "ExerciseDescription",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "MembershipProgramId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "TrainerId",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Workouts",
                newName: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupClassId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutType",
                table: "Workouts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                table: "MembershipPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutNumber",
                table: "MembershipPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutType",
                table: "MembershipPrograms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ClientMembershipPrograms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipProgramId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutRemainder = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMembershipPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientMembershipPrograms_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientMembershipPrograms_MembershipPrograms_MembershipProgramId",
                        column: x => x.MembershipProgramId,
                        principalTable: "MembershipPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientWorkout",
                columns: table => new
                {
                    ClientsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientWorkout", x => new { x.ClientsUserId, x.WorkoutsId });
                    table.ForeignKey(
                        name: "FK_ClientWorkout_Clients_ClientsUserId",
                        column: x => x.ClientsUserId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientWorkout_Workouts_WorkoutsId",
                        column: x => x.WorkoutsId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxClientNumber = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_GroupClassId",
                table: "Workouts",
                column: "GroupClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMembershipPrograms_ClientId",
                table: "ClientMembershipPrograms",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMembershipPrograms_MembershipProgramId",
                table: "ClientMembershipPrograms",
                column: "MembershipProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientWorkout_WorkoutsId",
                table: "ClientWorkout",
                column: "WorkoutsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_GroupClasses_GroupClassId",
                table: "Workouts",
                column: "GroupClassId",
                principalTable: "GroupClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_GroupClasses_GroupClassId",
                table: "Workouts");

            migrationBuilder.DropTable(
                name: "ClientMembershipPrograms");

            migrationBuilder.DropTable(
                name: "ClientWorkout");

            migrationBuilder.DropTable(
                name: "GroupClasses");

            migrationBuilder.DropIndex(
                name: "IX_Workouts_GroupClassId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "GroupClassId",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "Workouts");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "MembershipPrograms");

            migrationBuilder.DropColumn(
                name: "WorkoutNumber",
                table: "MembershipPrograms");

            migrationBuilder.DropColumn(
                name: "WorkoutType",
                table: "MembershipPrograms");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "Workouts",
                newName: "Title");

            migrationBuilder.AlterColumn<Guid>(
                name: "TrainerId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                table: "Workouts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ExerciseDescription",
                table: "Workouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "MembershipProgramId",
                table: "Clients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "TrainerId",
                table: "Activities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ActivityClient",
                columns: table => new
                {
                    ActivitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientsUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityClient", x => new { x.ActivitiesId, x.ClientsUserId });
                    table.ForeignKey(
                        name: "FK_ActivityClient_Activities_ActivitiesId",
                        column: x => x.ActivitiesId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityClient_Clients_ClientsUserId",
                        column: x => x.ClientsUserId,
                        principalTable: "Clients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_ClientId",
                table: "Workouts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MembershipProgramId",
                table: "Clients",
                column: "MembershipProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_TrainerId",
                table: "Activities",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityClient_ClientsUserId",
                table: "ActivityClient",
                column: "ClientsUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Trainers_TrainerId",
                table: "Activities",
                column: "TrainerId",
                principalTable: "Trainers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Clients_MembershipPrograms_MembershipProgramId",
                table: "Clients",
                column: "MembershipProgramId",
                principalTable: "MembershipPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_Clients_ClientId",
                table: "Workouts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
