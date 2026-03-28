using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerHub.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRequiredOwnerFieldFromOrganizationAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationOwnerId",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations",
                column: "OrganizationOwnerId",
                unique: true,
                filter: "[OrganizationOwnerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations");

            migrationBuilder.AlterColumn<string>(
                name: "OrganizationOwnerId",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations",
                column: "OrganizationOwnerId",
                unique: true);
        }
    }
}
