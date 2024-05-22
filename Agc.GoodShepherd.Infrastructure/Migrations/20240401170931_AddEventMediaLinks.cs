using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agc.GoodShepherd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEventMediaLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TukioMedia_TukioId",
                table: "TukioMedia");

            migrationBuilder.CreateIndex(
                name: "IX_TukioMedia_TukioId",
                table: "TukioMedia",
                column: "TukioId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TukioMedia_TukioId",
                table: "TukioMedia");

            migrationBuilder.CreateIndex(
                name: "IX_TukioMedia_TukioId",
                table: "TukioMedia",
                column: "TukioId");
        }
    }
}
