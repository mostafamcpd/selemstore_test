using Microsoft.AspNetCore.Mvc;
using selemstore_test.Data;
using selemstore_test.Models;
using System.Collections.Generic;
using System.Linq;

namespace selemstore_test.Controllers
{
    public class CartController : Controller
    {
        private const string SessionCartKey = "CartItems";
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // عرض سلة المشتريات
        public IActionResult Index()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

            if (!cartItems.Any())
            {
                ViewBag.Message = "سلتك فاضية حالياً.";
                ViewBag.TotalPrice = 0;
                ViewBag.TotalQuantity = 0;
                return View(cartItems);
            }

            decimal totalPrice = 0;
            int totalQuantity = 0;

            foreach (var item in cartItems)
            {
                // تحميل بيانات المنتج من قاعدة البيانات
                item.Product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId)
                               ?? new Product { Name = "غير متوفر", Sale_Price = 0, MainImage = "/images/default.png" };

                // تحميل بيانات المقاس من قاعدة البيانات
                item.Size = _context.Sizes.Where(s => s.SizeId == item.SizeId)
                                          .Select(s => s.Value)
                                          .FirstOrDefault() ?? "غير محدد";

                // تحميل بيانات اللون من قاعدة البيانات
                item.Color = _context.Colors.Where(c => c.ColorId == item.ColorId)
                                            .Select(c => c.ColorName)
                                            .FirstOrDefault() ?? "غير محدد";

                // حساب الإجمالي لكل منتج وضمه إلى الإجمالي العام
                totalPrice += item.Quantity * item.Product.Sale_Price.GetValueOrDefault();
                totalQuantity += item.Quantity; // حساب عدد القطع الإجمالي
            }

            ViewBag.TotalPrice = totalPrice; // إرسال الإجمالي إلى الفيو
            ViewBag.TotalQuantity = totalQuantity; // إرسال عدد القطع الإجمالي إلى الفيو

            return View(cartItems);
        }




        // إضافة منتج للسلة
        //[HttpPost]
        //public IActionResult AddToCart(int productId, int quantity, string size, string color)
        //{
        //    if (productId <= 0) return Json(new { success = false, message = "معرّف المنتج غير صالح." });
        //    if (quantity <= 0) return Json(new { success = false, message = "يرجى تحديد كمية صحيحة." });
        //    if (string.IsNullOrEmpty(size)) return Json(new { success = false, message = "يرجى اختيار المقاس." });
        //    if (string.IsNullOrEmpty(color)) return Json(new { success = false, message = "يرجى اختيار اللون." });

        //    var product = _context.Products.Find(productId);
        //    if (product == null) return Json(new { success = false, message = "هذا المنتج غير متوفر." });

        //    var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

        //    var existingItem = cartItems.FirstOrDefault(c => c.ProductId == productId && c.Size == size && c.Color == color);

        //    if (existingItem != null)
        //    {
        //        existingItem.Quantity += quantity;
        //    }
        //    else
        //    {
        //        cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, Size = size, Color = color });
        //    }

        //    HttpContext.Session.SetObjectAsJson(SessionCartKey, cartItems);

        //    return Json(new
        //    {
        //        success = true,
        //        message = "تمت إضافة المنتج إلى سلة المشتريات بنجاح!",
        //        options = new { continueShopping = "متابعة التسوق", goToCart = "الذهاب إلى سلة المشتريات" }
        //    });
        //}
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity, int sizeId, int colorId)
        {
            if (productId <= 0) return Json(new { success = false, message = "معرّف المنتج غير صالح." });
            if (quantity <= 0) return Json(new { success = false, message = "يرجى تحديد كمية صحيحة." });
            if (sizeId <= 0) return Json(new { success = false, message = "يرجى اختيار المقاس." });
            if (colorId <= 0) return Json(new { success = false, message = "يرجى اختيار اللون." });

            var product = _context.Products.Find(productId);
            if (product == null) return Json(new { success = false, message = "هذا المنتج غير متوفر." });

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

            var existingItem = cartItems.FirstOrDefault(c => c.ProductId == productId && c.SizeId == sizeId && c.ColorId == colorId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                var size = _context.Sizes.FirstOrDefault(s => s.SizeId == sizeId)?.Value ?? "غير محدد";
                var color = _context.Colors.FirstOrDefault(c => c.ColorId == colorId)?.ColorName ?? "غير محدد";

                cartItems.Add(new CartItem { ProductId = productId, Quantity = quantity, SizeId = sizeId, ColorId = colorId, Size = size, Color = color });
            }

            HttpContext.Session.SetObjectAsJson(SessionCartKey, cartItems);

            return Json(new
            {
                success = true,
                message = "تمت إضافة المنتج إلى سلة المشتريات بنجاح!",
                cartCount = cartItems.Count
            });
        }

        // حذف منتج من السلة
        [HttpPost]
        public IActionResult RemoveFromCart(int productId, int sizeId, int colorId)
        {
            if (productId <= 0) return Json(new { success = false, message = "معرّف المنتج غير صالح." });
            if (sizeId <= 0) return Json(new { success = false, message = "يرجى اختيار المقاس." });
            if (colorId <= 0) return Json(new { success = false, message = "يرجى اختيار اللون." });

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

            var itemToRemove = cartItems.FirstOrDefault(c =>
                c.ProductId == productId &&
                c.SizeId == sizeId &&
                c.ColorId == colorId);

            if (itemToRemove == null)
            {
                return Json(new { success = false, message = "المنتج غير موجود بالسلة." });
            }

            cartItems.Remove(itemToRemove);
            HttpContext.Session.SetObjectAsJson(SessionCartKey, cartItems);

            return Json(new
            {
                success = true,
                message = "تمت إزالة المنتج من السلة بنجاح!",
                cartCount = cartItems.Count
            });
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int sizeId, int colorId, int quantity)
        {
            if (productId <= 0 || quantity < 1)
                return Json(new { success = false, message = "بيانات غير صحيحة" });

            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

            var itemToUpdate = cartItems.FirstOrDefault(c =>
                c.ProductId == productId && c.SizeId == sizeId && c.ColorId == colorId);

            if (itemToUpdate == null)
                return Json(new { success = false, message = "المنتج غير موجود في السلة." });

            itemToUpdate.Quantity = quantity;
            HttpContext.Session.SetObjectAsJson(SessionCartKey, cartItems);

            return Json(new { success = true });
        }



        // إتمام الشراء
        public IActionResult Checkout()
        {
            var cartItems = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionCartKey) ?? new List<CartItem>();

            if (!cartItems.Any())
            {
                ViewBag.Message = "لا توجد منتجات لإتمام الشراء.";
                return RedirectToAction("Index");
            }

            HttpContext.Session.Remove(SessionCartKey);
            ViewBag.Message = "تم إتمام عملية الشراء بنجاح!";
            return View();
        }
    }
}
