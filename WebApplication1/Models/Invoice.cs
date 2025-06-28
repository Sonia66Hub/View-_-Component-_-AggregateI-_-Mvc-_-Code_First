using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Invoice
    {
            public int InvoiceId { get; set; }
           [Display(Name = "CustomerName")]
            public string CustomerName { get; set; } = null!;
           [Required, Display(Name = "Invoice Date"), DataType(DataType.Date), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
            public DateTime InvoiceDate { get; set; }
            public string InvoiceNo { get; set; } = null!;
            public bool IsPaid { get; set; }
            public string? ImageUrl { get; set; }
            public int CustomerTypeId { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal InvoiceAmount { get; set; }
        public virtual CustomerType CustomerType { get; set; } = null!;
            public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
     }

     public class InvoiceDetail
     {
            public int InvoiceDetailId { get; set; }
            public string ProductName { get; set; } = null!;
            public int Quantity { get; set; }
            public int InvoiceId { get; set; }
            public virtual Invoice? Invoice { get; set; } = null!;
      }
     public class CustomerType
     {
            public int CustomerTypeId { get; set; }
            public string CustomerTypeName { get; set; } = null!;
            public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
     }
     public class AppDbContext : DbContext
     {
        public AppDbContext(DbContextOptions<AppDbContext> op) : base(op)
        { }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<CustomerType> CustomerTypes { get; set; }
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerType>().HasData(
              new CustomerType { CustomerTypeId = 1, CustomerTypeName = "Active" },
                new CustomerType { CustomerTypeId = 2, CustomerTypeName = "Reglura" },
                new CustomerType { CustomerTypeId = 3, CustomerTypeName = "Premium" }
            );
        }
     }
}
