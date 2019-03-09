using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Customers.Pn.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string autoIDGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIDGenStrategyValue = SqlServerValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider == "Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIDGenStrategy = "MySql:ValueGenerationStrategy";
                autoIDGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyAddress = table.Column<string>(maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: true),
                    CityName = table.Column<string>(maxLength: 250, nullable: true),
                    Phone = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RelatedEntityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    RelatedEntityGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerVersions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Created_at = table.Column<DateTime>(nullable: true),
                    Updated_at = table.Column<DateTime>(nullable: true),
                    Workflow_state = table.Column<string>(maxLength: 255, nullable: true),
                    Version = table.Column<int>(nullable: false),
                    Created_By_User_Id = table.Column<int>(nullable: false),
                    Updated_By_User_Id = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(maxLength: 250, nullable: true),
                    CustomerNo = table.Column<string>(nullable: true),
                    CompanyName = table.Column<string>(maxLength: 250, nullable: true),
                    CompanyAddress = table.Column<string>(maxLength: 250, nullable: true),
                    ZipCode = table.Column<string>(maxLength: 50, nullable: true),
                    CityName = table.Column<string>(maxLength: 250, nullable: true),
                    Phone = table.Column<string>(maxLength: 250, nullable: true),
                    Email = table.Column<string>(maxLength: 250, nullable: true),
                    ContactPerson = table.Column<string>(maxLength: 250, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RelatedEntityId = table.Column<int>(nullable: true),
                    CustomerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerVersions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    Name = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFields",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    FieldId = table.Column<int>(nullable: false),
                    FieldStatus = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFields_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFields_FieldId",
                table: "CustomerFields",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RelatedEntityId",
                table: "Customers",
                column: "RelatedEntityId",
                unique: true,
                filter: "[RelatedEntityId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_Name",
                table: "Fields",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerFields");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "CustomerSettings");

            migrationBuilder.DropTable(
                name: "CustomerVersions");

            migrationBuilder.DropTable(
                name: "Fields");
        }
    }
}
