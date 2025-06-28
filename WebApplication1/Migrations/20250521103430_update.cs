using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE EditParamModuleType AS TABLE(
             ProductName NVARCHAR(50),
             Quantity INT
         );
        GO
        CREATE OR ALTER PROCEDURE dbo.spUpdateInvoice

            @CustomerName NVARCHAR(50),
            @InvoiceDate DATE,
            @InvoiceNo NVARCHAR(15),
            @CustomerTypeId INT,                
            @IsPaid BIT,
            @InvoiceAmount DECIMAL(18,4),
            @ImageUrl NVARCHAR(100),
            @InvoiceDetails EditParamModuleType READONLY,
            @InvoiceId INT
         AS
         BEGIN
         BEGIN TRY
        
 
        UPDATE dbo.Invoices
         SET
             CustomerName = @CustomerName,
             InvoiceDate = @InvoiceDate,
             InvoiceNo = @InvoiceNo,
             CustomerTypeId = @CustomerTypeId,
             IsPaid = @IsPaid,
             InvoiceAmount = @InvoiceAmount,
             ImageUrl = @ImageUrl
         WHERE
          InvoiceId = @InvoiceId;
         DELETE FROM dbo.InvoiceDetails
         WHERE InvoiceId = @InvoiceId;

        INSERT INTO dbo.InvoiceDetails(ProductName, Quantity, InvoiceId)
         SELECT ProductName, Quantity, @InvoiceId
         FROM @InvoiceDetails;

         END TRY
         BEGIN CATCH
             Throw;
         END CATCH
         END
     ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spUpdateInvoice");
            migrationBuilder.Sql("DROP TYPE IF EXISTS EditParamModuleType");
        }
    }
}
