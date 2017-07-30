using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirstApp.Data.Migrations
{
    public partial class AddThreadAndReplies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Thread",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CreatorId = table.Column<int>(nullable: false),
                    Topic = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thread", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Thread_User_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadReply",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Anonymous = table.Column<bool>(nullable: false),
                    AuthorId = table.Column<int>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    PosterId = table.Column<string>(nullable: true),
                    PosterName = table.Column<string>(nullable: true),
                    ThreadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadReply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadReply_Thread_Id",
                        column: x => x.Id,
                        principalTable: "Thread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThreadReply_Thread_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Thread",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Thread_CreatorId",
                table: "Thread",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadReply_ThreadId",
                table: "ThreadReply",
                column: "ThreadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadReply");

            migrationBuilder.DropTable(
                name: "Thread");
        }
    }
}
