using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigWithAPlan.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryGroupModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "CategoryGroups",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "Favorite",
                table: "Budgets",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryGroups_BudgetId",
                table: "CategoryGroups",
                column: "BudgetId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryGroups_Budgets_BudgetId",
                table: "CategoryGroups",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryGroups_Budgets_BudgetId",
                table: "CategoryGroups");

            migrationBuilder.DropIndex(
                name: "IX_CategoryGroups_BudgetId",
                table: "CategoryGroups");

            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "CategoryGroups");

            migrationBuilder.AlterColumn<bool>(
                name: "Favorite",
                table: "Budgets",
                type: "boolean",
                nullable: true,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);
        }
    }
}
