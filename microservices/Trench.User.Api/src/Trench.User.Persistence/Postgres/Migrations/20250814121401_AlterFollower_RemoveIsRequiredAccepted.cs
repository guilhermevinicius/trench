using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trench.User.Persistence.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AlterFollower_RemoveIsRequiredAccepted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "accepted",
                table: "Followers",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "accepted",
                table: "Followers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);
        }
    }
}
