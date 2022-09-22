using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agreement.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ProductGroups",
                columns: table => new
                {
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProductGroups", x => x.ProductGroupId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_tbl_Products_tbl_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tbl_ProductGroups",
                        principalColumn: "ProductGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Agreements",
                columns: table => new
                {
                    AgreementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NewPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Agreements", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK_tbl_Agreements_tbl_ProductGroups_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tbl_ProductGroups",
                        principalColumn: "ProductGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Agreements_tbl_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Products",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreements_ProductGroupId",
                table: "tbl_Agreements",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreements_ProductId",
                table: "tbl_Agreements",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreements_UserId",
                table: "tbl_Agreements",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_ProductGroups_GroupCode",
                table: "tbl_ProductGroups",
                column: "GroupCode",
                unique: true,
                filter: "[GroupCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Products_ProductGroupId",
                table: "tbl_Products",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Products_ProductNumber",
                table: "tbl_Products",
                column: "ProductNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Agreements");

            migrationBuilder.DropTable(
                name: "tbl_Products");

            migrationBuilder.DropTable(
                name: "tbl_ProductGroups");
        }
    }
}
