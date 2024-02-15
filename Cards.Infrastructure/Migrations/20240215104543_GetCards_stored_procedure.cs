using Cards.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cards.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GetCards_stored_procedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.MigrateSqlScripts();
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
