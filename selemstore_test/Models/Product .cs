using System.Collections.Generic;

namespace selemstore_test.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Sale_Price { get; set; } = 0;
        public int CategoryId { get; set; }
        public string MainImage { get; set; }

        // العلاقات
        public Category Category { get; set; }
        public List<ProductImage> ProductImages { get; set; }

        // استبدال ProductColors بـ ProductSizeColor
        public List<ProductSizeColor> ProductSizeColors { get; set; }
    }
}