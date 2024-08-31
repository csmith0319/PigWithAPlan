using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PigWithAPlan.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryGroupModel2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryGroupId1",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryGroupId1",
                table: "Categories",
                column: "CategoryGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId1",
                table: "Categories",
                column: "CategoryGroupId1",
                principalTable: "CategoryGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_CategoryGroups_CategoryGroupId1",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CategoryGroupId1",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "CategoryGroupId1",
                table: "Categories");
        }
    }
}
