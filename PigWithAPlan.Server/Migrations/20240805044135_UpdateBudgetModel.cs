using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigWithAPlan.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBudgetModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Budgets",
                type: "boolean",
                nullable: true,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Budgets");
        }
    }
}
