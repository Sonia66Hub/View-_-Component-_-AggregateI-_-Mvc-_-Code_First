using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ViewModels;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
    public class AggregateInfoViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public AggregateInfoViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _db.Invoices
                .Join(_db.CustomerTypes, p => p.CustomerTypeId, c => c.CustomerTypeId,
                      (p, c) => new { Invoice = p, CustomerType = c })
                .ToList();

            if (data.Count > 0)
            {
                var min = data.Min(p => p.Invoice.InvoiceAmount);
                var max = data.Max(p => p.Invoice.InvoiceAmount);
                var sum = data.Sum(p => p.Invoice.InvoiceAmount);
                var avg = data.Average(p => p.Invoice.InvoiceAmount);

                var groupByResult = data
                    .GroupBy(p => new { p.Invoice.CustomerTypeId, p.CustomerType.CustomerTypeName })
                    .Select(c => new GroupByViewModel
                    {
                        CustomerTypeId = c.Key.CustomerTypeId,
                        CustomerTypeName = c.Key.CustomerTypeName,
                        MaxValue = c.Max(p => p.Invoice.InvoiceAmount),
                        MinValue = c.Min(p => p.Invoice.InvoiceAmount),
                        SumValue = c.Sum(p => p.Invoice.InvoiceAmount),
                        AvgValue = c.Average(p => p.Invoice.InvoiceAmount),
                        Count = c.Count()
                    }).ToList();

                var aggregateViewModel = new AggregateViewModel
                {
                    MinValue = min,
                    MaxValue = max,
                    SumValue = sum,
                    AvgValue = avg,
                    GroupByResult = groupByResult
                };

                return View(aggregateViewModel);
            }


            return View(new AggregateViewModel
            {
                MinValue = 0,
                MaxValue = 0,
                SumValue = 0,
                AvgValue = 0,
                GroupByResult = new List<GroupByViewModel>()
            });
        }

    }
}


