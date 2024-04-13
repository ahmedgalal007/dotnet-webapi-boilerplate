using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddAuditableLocalizedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedNews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedNews",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Media",
                table: "LocalizedMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Media",
                table: "LocalizedMedia",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Article",
                table: "LocalizedKeyword",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Catalog",
                table: "LocalizedEditor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Catalog",
                table: "LocalizedEditor",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedCategory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedNews");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedNews");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Media",
                table: "LocalizedMedia");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Media",
                table: "LocalizedMedia");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Catalog",
                table: "LocalizedEditor");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Catalog",
                table: "LocalizedEditor");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedCategory");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedCategory");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "Article",
                table: "LocalizedKeyword",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
