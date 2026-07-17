using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppDeMensagem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    ChatType = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Chat_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "UsersChat",
                columns: table => new
                {
                    UserChat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersChat", x => x.UserChat_ID);
                    table.ForeignKey(
                        name: "FK_UsersChat_Chats_Chat_ID",
                        column: x => x.Chat_ID,
                        principalTable: "Chats",
                        principalColumn: "Chat_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersChat_Users_User_ID",
                        column: x => x.User_ID,
                        principalTable: "Users",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Message_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Chat_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sender_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(3000)", maxLength: 3000, nullable: false),
                    SendTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Message_ID);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_Chat_ID",
                        column: x => x.Chat_ID,
                        principalTable: "Chats",
                        principalColumn: "Chat_ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_UsersChat_Sender_ID",
                        column: x => x.Sender_ID,
                        principalTable: "UsersChat",
                        principalColumn: "UserChat_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Chat_ID",
                table: "Messages",
                column: "Chat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_Sender_ID",
                table: "Messages",
                column: "Sender_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersChat_Chat_ID",
                table: "UsersChat",
                column: "Chat_ID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersChat_User_ID",
                table: "UsersChat",
                column: "User_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UsersChat");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
