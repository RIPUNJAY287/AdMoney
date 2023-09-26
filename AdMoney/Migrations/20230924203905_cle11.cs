using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdMoney.Migrations
{
    public partial class cle11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "modelId",
                table: "Clients",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "modelId",
                table: "Clients");
        }
    }
}
