using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using selemstore_test.Data; // تأكد إن المسار مطابق لمشروعك
using selemstore_test.Models; // تأكد من المسار الصحيح للموديلات
using System.Linq;

namespace selemstore_test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // جلب الفئات مع المنتجات الخاصة بكل فئة
            var categories = _context.Categories
                .Include(c => c.Products) // جلب المنتجات المرتبطة بكل فئة
                .Where(c => c.Products.Any()) // عرض الفئات اللي فيها منتجات فقط
                .ToList();

            return View(categories);
        }
    }
}