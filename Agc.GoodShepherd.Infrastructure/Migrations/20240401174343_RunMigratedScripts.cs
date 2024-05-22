using Agc.GoodShepherd.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agc.GoodShepherd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RunMigratedScripts : Migration
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
