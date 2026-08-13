using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDeMensagem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoNomeChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameChat",
                table: "UsersChat",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameChat",
                table: "UsersChat");
        }
    }
}
