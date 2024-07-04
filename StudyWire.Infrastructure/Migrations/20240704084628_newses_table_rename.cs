using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudyWire.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newses_table_rename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_News_Schools_SchoolId",
                table: "News");

            migrationBuilder.DropPrimaryKey(
                name: "PK_News",
                table: "News");

            migrationBuilder.RenameTable(
                name: "News",
                newName: "Newses");

            migrationBuilder.RenameIndex(
                name: "IX_News_SchoolId",
                table: "Newses",
                newName: "IX_Newses_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Newses",
                table: "Newses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Newses_Schools_SchoolId",
                table: "Newses",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Newses_Schools_SchoolId",
                table: "Newses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Newses",
                table: "Newses");

            migrationBuilder.RenameTable(
                name: "Newses",
                newName: "News");

            migrationBuilder.RenameIndex(
                name: "IX_Newses_SchoolId",
                table: "News",
                newName: "IX_News_SchoolId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_News",
                table: "News",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_News_Schools_SchoolId",
                table: "News",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
