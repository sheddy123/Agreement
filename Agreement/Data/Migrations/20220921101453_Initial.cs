using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agreement.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Agreements_tbl_Products_ProductId",
                table: "tbl_Agreements");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Agreements_tbl_Products_ProductId",
                table: "tbl_Agreements",
                column: "ProductId",
                principalTable: "tbl_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbl_Agreements_tbl_Products_ProductId",
                table: "tbl_Agreements");

            migrationBuilder.AddForeignKey(
                name: "FK_tbl_Agreements_tbl_Products_ProductId",
                table: "tbl_Agreements",
                column: "ProductId",
                principalTable: "tbl_Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
