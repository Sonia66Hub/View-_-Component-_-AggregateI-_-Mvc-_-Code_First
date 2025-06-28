using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.ViewModels
{
    public class InvoiceViewModel
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
        public virtual CustomerType? CustomerType { get; set; } = null!;
       
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; } = new List<InvoiceDetail>();
        public virtual IList<CustomerType>? CustomerTypes { get; set; } = null!;
        public IFormFile? ProfileFile { get; set; } = null!;

    }
}
