using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.EmailSender.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailSenderInfo",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSenderInfo", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recipients = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FailedMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailSenderItemId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Mails_EmailSenderInfo_EmailSenderItemId",
                        column: x => x.EmailSenderItemId,
                        principalTable: "EmailSenderInfo",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mails_EmailSenderItemId",
                table: "Mails",
                column: "EmailSenderItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "EmailSenderInfo");
        }
    }
}
