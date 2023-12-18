using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PulseGym.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DefaultAdminInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO AspNetUsers(Id, 
                                        FirstName, 
                                        LastName,
                                        UserName,
                                        NormalizedUserName,
                                        Birthday, 
                                        ImageUrl, 
                                        Email, 
                                        NormalizedEmail,
                                        EmailConfirmed,
                                        PasswordHash,
                                        PhoneNumber, 
                                        PhoneNumberConfirmed, 
                                        AccessFailedCount, 
                                        ConcurrencyStamp,  
                                        SecurityStamp,
                                        LockoutEnabled, 
                                        LockoutEnd,    
                                        TwoFactorEnabled)
                VALUES ('A8CE6CBF-E581-467B-97B9-08DBF4ABE2A6', 
                        'Alina', 
                        'Admin', 
                        'admin', 
                        'ADMIN', 
                        '2003-01-30T14:25:10', 
                        null, 
                        'admin@pulse.com', 
                        'ADMIN@PULSE.COM', 
                        'false', 
                        'AQAAAAIAAYagAAAAEHRISIfWoVQzydcD5zitHlefgk4cO6VeCd262lIcf33czy3PH+LboI6SJJp2YN7kUg==',
                        null,
                        'false',
                        0,
                        null,
                        '3850a5e8-6ce6-4a24-82db-3f900579ab2c',
                        'true',
                        null,
                        'false')
            ");

            migrationBuilder.Sql(@"
                INSERT INTO AspNetUserRoles(UserId, RoleId)
                VALUES ('A8CE6CBF-E581-467B-97B9-08DBF4ABE2A6',(SELECT TOP 1 Id FROM dbo.AspNetRoles WHERE Name = 'admin') )
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
