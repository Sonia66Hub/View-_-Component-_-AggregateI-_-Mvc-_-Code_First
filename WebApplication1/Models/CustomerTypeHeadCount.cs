namespace WebApplication1.Models
{
    public class CustomerTypeHeadCount
    {
        public int CustomerTypeId { get; set; }
        public int Count { get; set; }
        public virtual CustomerType CustomerType { get; set; }
        public string CustomerTypeName { get; set; }
       
    }
}
