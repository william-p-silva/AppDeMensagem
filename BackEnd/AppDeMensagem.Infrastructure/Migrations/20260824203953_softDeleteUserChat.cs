using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDeMensagem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class softDeleteUserChat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "UsersChat",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "UsersChat");
        }
    }
}
