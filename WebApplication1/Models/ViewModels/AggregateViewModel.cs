namespace WebApplication1.Models.ViewModels
{
    public class AggregateViewModel
    {
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal SumValue { get; set; }
        public decimal AvgValue { get; set; }
        public ICollection<GroupByViewModel> GroupByResult { get; set; }
    }
    public class GroupByViewModel
    {
        public int CustomerTypeId { get; set; }
        public string CustomerTypeName { get; set; }
        public int Count { get; set; }
        public decimal MinValue { get; set; }
        public decimal MaxValue { get; set; }
        public decimal SumValue { get; set; }
        public decimal AvgValue { get; set; }
    }
}
