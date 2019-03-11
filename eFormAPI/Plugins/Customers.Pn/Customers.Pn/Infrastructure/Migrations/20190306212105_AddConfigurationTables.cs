using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Pn.Infrastructure.Migrations
{
    public partial class AddConfigurationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
            }

            migrationBuilder.CreateTable(
                name: "PluginConfigurationValues",
                columns: table => new
                {
                    Id = table.Column<string>(),
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
                name: "IX_PluginConfigurationVersions_Id",
                table: "PluginConfigurationVersions",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PluginConfigurationVersions_Id_Version",
                table: "PluginConfigurationVersions",
                columns: new[] { "Id", "Version" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginConfigurationValues");

            migrationBuilder.DropTable(
                name: "PluginConfigurationVersions");
        }
    }
}
