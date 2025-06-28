using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class sss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoicesDetails_Invoices_InvoiceId",
                table: "InvoicesDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoicesDetails",
                table: "InvoicesDetails");

            migrationBuilder.RenameTable(
                name: "InvoicesDetails",
                newName: "InvoiceDetails");

            migrationBuilder.RenameIndex(
                name: "IX_InvoicesDetails_InvoiceId",
                table: "InvoiceDetails",
                newName: "IX_InvoiceDetails_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails",
                column: "InvoiceDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceDetails_Invoices_InvoiceId",
                table: "InvoiceDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceDetails",
                table: "InvoiceDetails");

            migrationBuilder.RenameTable(
                name: "InvoiceDetails",
                newName: "InvoicesDetails");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceDetails_InvoiceId",
                table: "InvoicesDetails",
                newName: "IX_InvoicesDetails_InvoiceId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoicesDetails",
                table: "InvoicesDetails",
                column: "InvoiceDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoicesDetails_Invoices_InvoiceId",
                table: "InvoicesDetails",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
