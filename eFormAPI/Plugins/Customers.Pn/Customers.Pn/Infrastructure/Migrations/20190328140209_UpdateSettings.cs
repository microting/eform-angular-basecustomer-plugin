using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Pn.Infrastructure.Migrations
{
    public partial class UpdateSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string autoIdGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIdGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIdGenStrategy = "MySql:ValueGenerationStrategy";
                autoIdGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }

            migrationBuilder.DropTable(
                name: "PluginConfigurationVersions");

            migrationBuilder.DropIndex(
                name: "IX_PluginConfigurationValues_Id",
                table: "PluginConfigurationValues");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RelatedEntityId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "PluginConfigurationValues",
                nullable: false,
                oldClrType: typeof(string))
                .Annotation(autoIdGenStrategy, autoIdGenStrategyValue);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PluginConfigurationValues",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "PluginConfigurationValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PluginConfigurationValues",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PluginConfigurationValues",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "PluginConfigurationValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "PluginConfigurationValues",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WorkflowState",
                table: "PluginConfigurationValues",
                maxLength: 255,
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValueVersions",
                columns: table => new
                {
                    Id = table.Column<int>()
                        .Annotation(autoIdGenStrategy, autoIdGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(),
                    UpdatedByUserId = table.Column<int>(),
                    Version = table.Column<int>(),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginConfigurationValueVersions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RelatedEntityId",
                table: "Customers",
                column: "RelatedEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string autoIdGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIdGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIdGenStrategy = "MySql:ValueGenerationStrategy";
                autoIdGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }

            migrationBuilder.DropTable(
                name: "PluginConfigurationValueVersions");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RelatedEntityId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "PluginConfigurationValues");

            migrationBuilder.DropColumn(
                name: "WorkflowState",
                table: "PluginConfigurationValues");

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

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "PluginConfigurationValues",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation(autoIdGenStrategy, autoIdGenStrategyValue);

            migrationBuilder.CreateTable(
                name: "PluginConfigurationVersions",
                columns: table => new
                {
                    Id = table.Column<string>(),
                    Value = table.Column<string>(nullable: true),
                    Version = table.Column<int>()
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
                name: "IX_Customers_RelatedEntityId",
                table: "Customers",
                column: "RelatedEntityId",
                unique: true,
                filter: "[RelatedEntityId] IS NOT NULL");

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
