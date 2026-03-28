using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolunteerHub.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrganizationModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMembers_Organizations_OrganizationId",
                table: "OrganizationMembers");

            migrationBuilder.AddColumn<string>(
                name: "OrganizationOwnerId",
                table: "Organizations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations",
                column: "OrganizationOwnerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMembers_Organizations_OrganizationId",
                table: "OrganizationMembers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Organizations_Accounts_OrganizationOwnerId",
                table: "Organizations",
                column: "OrganizationOwnerId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrganizationMembers_Organizations_OrganizationId",
                table: "OrganizationMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Organizations_Accounts_OrganizationOwnerId",
                table: "Organizations");

            migrationBuilder.DropIndex(
                name: "IX_Organizations_OrganizationOwnerId",
                table: "Organizations");

            migrationBuilder.DropColumn(
                name: "OrganizationOwnerId",
                table: "Organizations");

            migrationBuilder.AddForeignKey(
                name: "FK_OrganizationMembers_Organizations_OrganizationId",
                table: "OrganizationMembers",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
