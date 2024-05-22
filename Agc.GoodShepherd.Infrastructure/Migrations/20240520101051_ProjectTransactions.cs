using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agc.GoodShepherd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProjectTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTransaction_MpesaPayment_MpesaPaymentId",
                table: "ProjectTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTransaction",
                table: "ProjectTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MpesaPayment",
                table: "MpesaPayment");

            migrationBuilder.RenameTable(
                name: "ProjectTransaction",
                newName: "ProjectTransactions");

            migrationBuilder.RenameTable(
                name: "MpesaPayment",
                newName: "MpesaPayments");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTransaction_MpesaPaymentId",
                table: "ProjectTransactions",
                newName: "IX_ProjectTransactions_MpesaPaymentId");

            migrationBuilder.AddColumn<string>(
                name: "ProjectId",
                table: "ProjectTransactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTransactions",
                table: "ProjectTransactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MpesaPayments",
                table: "MpesaPayments",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTransactions_ProjectId",
                table: "ProjectTransactions",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTransactions_MpesaPayments_MpesaPaymentId",
                table: "ProjectTransactions",
                column: "MpesaPaymentId",
                principalTable: "MpesaPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTransactions_Projects_ProjectId",
                table: "ProjectTransactions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTransactions_MpesaPayments_MpesaPaymentId",
                table: "ProjectTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectTransactions_Projects_ProjectId",
                table: "ProjectTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectTransactions",
                table: "ProjectTransactions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectTransactions_ProjectId",
                table: "ProjectTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MpesaPayments",
                table: "MpesaPayments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProjectTransactions");

            migrationBuilder.RenameTable(
                name: "ProjectTransactions",
                newName: "ProjectTransaction");

            migrationBuilder.RenameTable(
                name: "MpesaPayments",
                newName: "MpesaPayment");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectTransactions_MpesaPaymentId",
                table: "ProjectTransaction",
                newName: "IX_ProjectTransaction_MpesaPaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectTransaction",
                table: "ProjectTransaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MpesaPayment",
                table: "MpesaPayment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectTransaction_MpesaPayment_MpesaPaymentId",
                table: "ProjectTransaction",
                column: "MpesaPaymentId",
                principalTable: "MpesaPayment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
