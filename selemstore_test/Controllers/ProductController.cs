// إضافة مكتبات ضرورية
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using selemstore_test.Data;
using System.Linq;
using System.Threading.Tasks;

namespace selemstore_test.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض قائمة المنتجات مع دعم البحث والتصفية حسب الفئة
        public IActionResult Index(string searchTerm, int? categoryId)
        {
            var categoriesQuery = _context.Categories
                                          .Include(c => c.Products)
                                              .ThenInclude(p => p.ProductImages)
                                          .AsQueryable();

            if (categoryId.HasValue)
            {
                categoriesQuery = categoriesQuery.Where(c => c.CategoryId == categoryId.Value);
            }

            var categories = categoriesQuery.ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                foreach (var category in categories)
                {
                    category.Products = category.Products
                                                .Where(p => p.Name.Contains(searchTerm) ||
                                                            p.Description.Contains(searchTerm))
                                                .ToList();
                }
            }

            ViewBag.SearchTerm = searchTerm;
            ViewBag.SelectedCategoryId = categoryId;

            return View(categories);
        }

        // عرض تفاصيل المنتج مع المقاسات والألوان المتاحة
        public IActionResult ProductDetails(int id)
        {
            var product = _context.Products
                                  .Include(p => p.ProductImages)
                                  .Include(p => p.ProductSizeColors)
                                      .ThenInclude(psc => psc.Size)
                                  .Include(p => p.ProductSizeColors)
                                      .ThenInclude(psc => psc.Color)
                                  .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        //// ✅ API لجلب السعر حسب المقاس واللون
        //[HttpGet]
        //public async Task<IActionResult> GetPrice(int productId, int sizeId, int colorId)
        //{
        //    var price = await _context.ProductSizeColors
        //        .Where(psc => psc.ProductId == productId && psc.SizeId == sizeId && psc.ColorId == colorId)
        //        .Select(psc => psc.SellingPrice) 
        //        .FirstOrDefaultAsync();

        //    if (price == 0)
        //        return NotFound();

        //    return Json(new { price });
        //}
    }
}
