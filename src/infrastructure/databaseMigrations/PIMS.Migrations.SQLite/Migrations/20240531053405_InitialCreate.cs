using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMS.Migrations.SQLite.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    ActiveTaskID = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTasks",
                columns: table => new
                {
                    ID = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    PreviousTaskID = table.Column<string>(type: "TEXT", nullable: true),
                    DocumentID = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_Documents_DocumentID",
                        column: x => x.DocumentID,
                        principalTable: "Documents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTasks_ProjectTasks_PreviousTaskID",
                        column: x => x.PreviousTaskID,
                        principalTable: "ProjectTasks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ActiveTaskID",
                table: "Documents",
                column: "ActiveTaskID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_DocumentID",
                table: "ProjectTasks",
                column: "DocumentID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTasks_PreviousTaskID",
                table: "ProjectTasks",
                column: "PreviousTaskID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_ProjectTasks_ActiveTaskID",
                table: "Documents",
                column: "ActiveTaskID",
                principalTable: "ProjectTasks",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_ProjectTasks_ActiveTaskID",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "ProjectTasks");

            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
