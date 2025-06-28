using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _web;

        public InvoicesController(AppDbContext db, IWebHostEnvironment web)
        {
            _db = db;
            _web = web;
        }

        public IActionResult Index()
        {
            var invoices = _db.Invoices.Include(s => s.CustomerType).Include(s => s.InvoiceDetails).ToList();
            return View(invoices);
           
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var invoice = _db.Invoices.Find(id);
            if (invoice != null)
            {
                _db.Invoices.Remove(invoice);
                _db.SaveChanges();

            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateInvoice()
        {
             InvoiceViewModel invoice = new InvoiceViewModel();
            invoice.CustomerTypes = _db.CustomerTypes.ToList();
            invoice.InvoiceDetails.Add(new InvoiceDetail() { InvoiceDetailId = 1 });
            return PartialView("_CreateInvoicePartial", invoice);

        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task <ActionResult> CreateInvoice(InvoiceViewModel vobj)
        {
            if (!ModelState.IsValid)
            {
                vobj.CustomerTypes = _db.CustomerTypes.ToList();
                return View();
            }
            Invoice invoice = new Invoice
            {
                CustomerName = vobj.CustomerName,
                InvoiceDate = vobj.InvoiceDate,
                InvoiceNo = vobj.InvoiceNo,
                CustomerTypeId = vobj.CustomerTypeId,
                IsPaid = vobj.IsPaid,
                InvoiceAmount = vobj.InvoiceAmount,
                InvoiceDetails = vobj.InvoiceDetails
            };

            if (vobj.ProfileFile != null)
            {
                string uniqueFileName = GetFileName(vobj.ProfileFile);
                invoice.ImageUrl = uniqueFileName;
            }
            else
            {
                invoice.ImageUrl = "noimage.png";
            }

          
            DataTable moduleTable = new DataTable();
            moduleTable.Columns.Add("ProductName", typeof(string));
            moduleTable.Columns.Add("Quantity", typeof(int));
            if (invoice.InvoiceDetails != null && invoice.InvoiceDetails.Any())
            {
                foreach (var m in invoice.InvoiceDetails)
                {
                    moduleTable.Rows.Add(m.ProductName, m.Quantity);
                }
            }
            var parameters = new[]
            {
                new SqlParameter("@CustomerName",invoice.CustomerName),
                new SqlParameter("@InvoiceDate",invoice.InvoiceDate),
                new SqlParameter("@InvoiceNo",invoice.InvoiceNo),
                new SqlParameter("@CustomerTypeId",invoice.CustomerTypeId),
                new SqlParameter("@IsPaid",invoice.IsPaid),
                new SqlParameter("@InvoiceAmount",invoice.InvoiceAmount),
                new SqlParameter("@ImageUrl",invoice.ImageUrl??(object)DBNull.Value),
                new SqlParameter
                {
                    ParameterName="@InvoiceDetails",
                    SqlDbType= SqlDbType.Structured,
                    TypeName="dbo.ParamModuleType",
                    Value=moduleTable
                }
            };
            await _db.Database.ExecuteSqlRawAsync("EXEC dbo.spInsertInvoice @CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@InvoiceAmount,@ImageUrl,@InvoiceDetails", parameters);
            return RedirectToAction("Index");

        }
        private string GetFileName(IFormFile profileFile)
        {
            string uniqueFileName = null;
            if (profileFile != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(profileFile.FileName); ;
                var uploadFolder = Path.Combine(_web.WebRootPath, "images");
                var filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    profileFile.CopyToAsync(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpGet]
        public ActionResult EditPartial(int id)
        {
            var invoice = _db.Invoices
                .Include(a => a.InvoiceDetails)
                .FirstOrDefault(x => x.InvoiceId == id);
            var vObj = new InvoiceViewModel
            {
                CustomerName = invoice.CustomerName,
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                InvoiceNo = invoice.InvoiceNo,
                CustomerTypeId = invoice.CustomerTypeId,
                IsPaid = invoice.IsPaid,
                ImageUrl = invoice.ImageUrl,
                InvoiceAmount = invoice.InvoiceAmount,
                InvoiceDetails = invoice.InvoiceDetails.ToList(),
                CustomerTypes = _db.CustomerTypes.ToList()
            };
            return PartialView("_EditInvoicePartial", vObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <ActionResult> EditInvoice(InvoiceViewModel vobj, string OldImageUrl)
        {
            if (!ModelState.IsValid)
            {
                vobj.CustomerTypes = _db.CustomerTypes.ToList();
                return Json(new { success = false });
            }
            Invoice invoice = _db.Invoices.Find(vobj.InvoiceId);
            if (invoice != null)
            {
                invoice.CustomerName = vobj.CustomerName;
                invoice.CustomerTypeId = vobj.CustomerTypeId;
                invoice.InvoiceNo = vobj.InvoiceNo;
                invoice.IsPaid = vobj.IsPaid;
                invoice.InvoiceDate = vobj.InvoiceDate;
               
                invoice.InvoiceAmount = vobj.InvoiceAmount;
                if (vobj.ProfileFile != null)
                {
                    string uniqueFileName = GetFileName(vobj.ProfileFile);
                    invoice.ImageUrl = uniqueFileName;
                }
                else
                {
                    invoice.ImageUrl = OldImageUrl;
                }
                var modules = _db.InvoiceDetails.Where(m => m.InvoiceId == vobj.InvoiceId).ToList();
                DataTable moduleTable = new DataTable();
                moduleTable.Columns.Add("ProductName", typeof(string));
                moduleTable.Columns.Add("Quantity", typeof(int));
                if (vobj.InvoiceDetails != null && vobj.InvoiceDetails.Any())
                {
                    foreach (var m in vobj.InvoiceDetails)
                    {
                        moduleTable.Rows.Add(m.ProductName, m.Quantity);
                    }
                }
                var parameters = new[]
                {
                new SqlParameter("@CustomerName",invoice.CustomerName),
                new SqlParameter("@InvoiceDate",invoice.InvoiceDate),
                new SqlParameter("@InvoiceNo",invoice.InvoiceNo),
                new SqlParameter("@CustomerTypeId",invoice.CustomerTypeId),
                new SqlParameter("@IsPaid",invoice.IsPaid),
                new SqlParameter("@InvoiceAmount",invoice.InvoiceAmount),
                new SqlParameter("@ImageUrl",invoice.ImageUrl??(object)DBNull.Value),
                new SqlParameter
                {
                    ParameterName="@InvoiceDetails",
                    SqlDbType= SqlDbType.Structured,
                    TypeName="dbo.EditParamModuleType",
                    Value=moduleTable
                },
                new SqlParameter("@InvoiceId",vobj.InvoiceId),               
            };
                await _db.Database.ExecuteSqlRawAsync("EXEC dbo.spUpdateInvoice @CustomerName,@InvoiceDate,@InvoiceNo,@CustomerTypeId,@IsPaid,@InvoiceAmount,@ImageUrl,@InvoiceDetails,@InvoiceId", parameters);
                
                _db.Entry(invoice).State = EntityState.Modified;
                _db.SaveChanges();
                try
                {

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    vobj.CustomerTypes = _db.CustomerTypes.ToList();
                    return Json(new { success = false });
                }
            }
            vobj.CustomerTypes = _db.CustomerTypes.ToList();
            return View(vobj);
        }
    }
}
