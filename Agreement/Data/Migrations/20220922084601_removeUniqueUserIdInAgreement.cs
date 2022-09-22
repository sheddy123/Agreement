using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agreement.Data.Migrations
{
    public partial class removeUniqueUserIdInAgreement : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_Agreements_UserId",
                table: "tbl_Agreements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tbl_Agreements",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "tbl_Agreements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreements_UserId",
                table: "tbl_Agreements",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }
    }
}
