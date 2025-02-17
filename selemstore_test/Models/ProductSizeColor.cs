namespace selemstore_test.Models
{
    public class ProductSizeColor
    {
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int Quantity { get; set; } // الكمية المتاحة لهذا اللون والمقاس

        public decimal CostPrice { get; set; }  // ✅ سعر الشراء لكل مقاس ولون
        public decimal SellingPrice { get; set; } // ✅ سعر البيع لكل مقاس ولون

        public Product Product { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
    }
}

