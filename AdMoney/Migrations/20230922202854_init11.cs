using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdMoney.Migrations
{
    public partial class init11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdvisorId",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskProfile",
                table: "Clients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvisorId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "RiskProfile",
                table: "Clients");
        }
    }
}
