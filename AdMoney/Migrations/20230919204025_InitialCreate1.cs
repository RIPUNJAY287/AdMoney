using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdMoney.Migrations
{
    public partial class InitialCreate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "security",
                table: "AssetSecurity");

            migrationBuilder.RenameColumn(
                name: "asset",
                table: "AssetSecurity",
                newName: "Asset");

            migrationBuilder.AlterColumn<string>(
                name: "Asset",
                table: "AssetSecurity",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SecurityName",
                table: "AssetSecurity",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(type: "int", nullable: false),
                    modelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserModels");

            migrationBuilder.DropColumn(
                name: "SecurityName",
                table: "AssetSecurity");

            migrationBuilder.RenameColumn(
                name: "Asset",
                table: "AssetSecurity",
                newName: "asset");

            migrationBuilder.AlterColumn<string>(
                name: "asset",
                table: "AssetSecurity",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddColumn<string>(
                name: "security",
                table: "AssetSecurity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
