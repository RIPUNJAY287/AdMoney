using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdMoney.Migrations
{
    public partial class c1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RiskProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Risk = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Thresold = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskProfiles", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RiskProfiles");
        }
    }
}
