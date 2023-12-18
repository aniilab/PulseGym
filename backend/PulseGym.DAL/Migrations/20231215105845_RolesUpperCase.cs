using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseGym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RolesUpperCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "MembershipPrograms",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialWeight",
                table: "Clients",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "InitialHeight",
                table: "Clients",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            //Add Roles
            migrationBuilder.Sql(@"
                INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp)
                VALUES ('48713e38-a6b4-4e53-90e7-49c9b8ca57c0','Admin', 'ADMIN', null), 
                       ('5f56b785-9c09-45ae-9464-fa602fd1673d','Trainer', 'TRAINER', null),
                       ('053fbfb1-e92c-4c2b-a1ad-563ae987a6b3','Client', 'CLIENT', null)
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "MembershipPrograms",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "InitialWeight",
                table: "Clients",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "InitialHeight",
                table: "Clients",
                type: "float",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);
        }
    }
}
