using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdMoney.Migrations
{
    public partial class InitialCreate7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Security",
                table: "AssetSecurity",
                newName: "SecurityName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SecurityName",
                table: "AssetSecurity",
                newName: "Security");
        }
    }
}
