using System.Collections.Generic;

namespace selemstore_test.Models
{
    public class Color
    {
        public int ColorId { get; set; }  // مفتاح أساسي
        public string ColorName { get; set; }  // اسم اللون

        public string HexCode {  get; set; }
        // علاقة الربط مع ProductSizeColor
        public List<ProductSizeColor> ProductSizeColors { get; set; } = new List<ProductSizeColor>();
    }
}