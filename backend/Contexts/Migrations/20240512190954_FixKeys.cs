using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Contexts.Migrations
{
    /// <inheritdoc />
    public partial class FixKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackedItemValues_TrackedItems_Id",
                table: "TrackedItemValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackedItemValues",
                table: "TrackedItemValues");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TrackedItemValues",
                newName: "TrackedItemId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TrackedItems",
                newName: "TrackedItemId");

            migrationBuilder.AddColumn<int>(
                name: "TrackedItemValueId",
                table: "TrackedItemValues",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackedItemValues",
                table: "TrackedItemValues",
                column: "TrackedItemValueId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackedItemValues_TrackedItemId",
                table: "TrackedItemValues",
                column: "TrackedItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackedItemValues_TrackedItems_TrackedItemId",
                table: "TrackedItemValues",
                column: "TrackedItemId",
                principalTable: "TrackedItems",
                principalColumn: "TrackedItemId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackedItemValues_TrackedItems_TrackedItemId",
                table: "TrackedItemValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackedItemValues",
                table: "TrackedItemValues");

            migrationBuilder.DropIndex(
                name: "IX_TrackedItemValues_TrackedItemId",
                table: "TrackedItemValues");

            migrationBuilder.DropColumn(
                name: "TrackedItemValueId",
                table: "TrackedItemValues");

            migrationBuilder.RenameColumn(
                name: "TrackedItemId",
                table: "TrackedItemValues",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TrackedItemId",
                table: "TrackedItems",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackedItemValues",
                table: "TrackedItemValues",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackedItemValues_TrackedItems_Id",
                table: "TrackedItemValues",
                column: "Id",
                principalTable: "TrackedItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
