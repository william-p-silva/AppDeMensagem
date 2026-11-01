using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDeMensagem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class statusUserChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsersChat");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UsersChat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UsersChat");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsersChat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
