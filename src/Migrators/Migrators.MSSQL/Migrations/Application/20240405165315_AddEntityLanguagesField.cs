using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    /// <inheritdoc />
    public partial class AddEntityLanguagesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Video_IsExternal",
                schema: "Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "IsExternal",
                schema: "Media",
                table: "ImageVersion");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "Media",
                table: "ImageVersion");

            migrationBuilder.RenameColumn(
                name: "VideoTitle",
                schema: "Media",
                table: "LocalizedMedia",
                newName: "ImageTitle");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "Article",
                table: "News",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Height",
                schema: "Media",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "Media",
                table: "Media",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Width",
                schema: "Media",
                table: "Media",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedKeyword",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedKeyword",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "Article",
                table: "Keyword",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "Catalog",
                table: "Editor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Languages",
                schema: "Article",
                table: "Category",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "Article",
                table: "News");

            migrationBuilder.DropColumn(
                name: "Height",
                schema: "Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Width",
                schema: "Media",
                table: "Media");

            migrationBuilder.DropColumn(
                name: "Enabled",
                schema: "Article",
                table: "LocalizedKeyword");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                schema: "Article",
                table: "LocalizedKeyword");

            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "Article",
                table: "Keyword");

            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "Catalog",
                table: "Editor");

            migrationBuilder.DropColumn(
                name: "Languages",
                schema: "Article",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ImageTitle",
                schema: "Media",
                table: "LocalizedMedia",
                newName: "VideoTitle");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Media",
                table: "Media",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Video_IsExternal",
                schema: "Media",
                table: "Media",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsExternal",
                schema: "Media",
                table: "ImageVersion",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "Media",
                table: "ImageVersion",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
