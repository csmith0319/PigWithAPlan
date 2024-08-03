using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigWithAPlan.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigurations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_Users_UserId1",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_UserId1",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Budgets");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Budgets",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId1",
                table: "Budgets",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_Users_UserId1",
                table: "Budgets",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
