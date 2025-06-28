using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class spInsert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE TYPE ParamModuleType AS TABLE(
                ProductName NVARCHAR(50),
                Quantity INT
            );
           GO
           CREATE OR ALTER PROCEDURE dbo.spInsertInvoice
            @CustomerName NVARCHAR(50),
            @InvoiceDate DATE,
            @InvoiceNo NVARCHAR(15),
            @CustomerTypeId INT,                
            @IsPaid BIT,
            @InvoiceAmount DECIMAL(18,4),
            @ImageUrl NVARCHAR(100),
            @InvoiceDetails ParamModuleType READONLY
            AS
            BEGIN
            BEGIN TRY
            DECLARE @LocalModules TABLE(
               ProductName NVARCHAR(50),
               Quantity INT,
               InvoiceId INT
             );
             DECLARE @InvoiceId INT;
            INSERT INTO dbo.Invoices(CustomerName, InvoiceDate,InvoiceNo,CustomerTypeId,IsPaid,InvoiceAmount,ImageUrl) VALUES (@CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@InvoiceAmount, @ImageUrl);
            SET @InvoiceId=SCOPE_IDENTITY();

            INSERT INTO @LocalModules(ProductName, Quantity, InvoiceId)
            SELECT ProductName, Quantity, @InvoiceId
            FROM @InvoiceDetails

            INSERT INTO dbo.InvoiceDetails(ProductName, Quantity, InvoiceId)
            SELECT ProductName, Quantity, @InvoiceId
            FROM @LocalModules

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
            migrationBuilder.Sql("DROP PROCUDURE IF EXISTS dbo.spInsertInvoice");
            migrationBuilder.Sql("DROP TYPE IF EXISTS ParamModuleType");
        }
    }
}
