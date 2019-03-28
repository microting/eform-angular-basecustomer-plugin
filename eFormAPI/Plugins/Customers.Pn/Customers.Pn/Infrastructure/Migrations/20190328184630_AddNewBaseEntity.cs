using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Pn.Infrastructure.Migrations
{
    public partial class AddNewBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginConfigurationValues");

            migrationBuilder.DropTable(
                name: "PluginConfigurationVersions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Fields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Fields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "Fields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "Fields",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomerVersions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "CustomerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CustomerVersions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "CustomerVersions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "CustomerVersions",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Customers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "Customers",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CustomerFields",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "CustomerFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CustomerFields",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "CustomerFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CustomerFields",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "CustomerFields",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomerVersions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CustomerVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CustomerVersions");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "CustomerVersions");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "CustomerVersions");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CustomerFields");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "CustomerFields");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CustomerFields");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "CustomerFields");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CustomerFields");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "CustomerFields");

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValues",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PluginConfigurationVersions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Version = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationVersions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfigurationValues_Id",
                table: "PluginConfigurationValues",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfigurationVersions_Id",
                table: "PluginConfigurationVersions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfigurationVersions_Id_Version",
                table: "PluginConfigurationVersions",
                columns: new[] { "Id", "Version" },
                unique: true);
        }
    }
}
