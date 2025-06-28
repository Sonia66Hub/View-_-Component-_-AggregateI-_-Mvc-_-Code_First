using WebApplication1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreMasterDetails.ViewComponents
{
    public class HeadCountViewComponent : ViewComponent
    {
        private readonly AppDbContext _db;
        public HeadCountViewComponent(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync(int customerTypeId)
        {
            var customerTypeCounts = await _db.Invoices.Include(s => s.CustomerType).GroupBy(s => new { s.CustomerType.CustomerTypeId, s.CustomerType.CustomerTypeName }).Select(g => new CustomerTypeHeadCount
            {
                CustomerTypeId = g.Key.CustomerTypeId,
                CustomerTypeName = g.Key.CustomerTypeName,
                Count = g.Count()
            }).ToListAsync();
            return View(customerTypeCounts);
        }
    }
}
