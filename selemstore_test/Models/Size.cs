using System.Collections.Generic;

namespace selemstore_test.Models
{
    public class Size
    {
        public int SizeId { get; set; }  // مفتاح أساسي
        public string Value { get; set; }  // قيمة المقاس (مثل S, M, L)

        // علاقة الربط مع ProductSizeColor
        public List<ProductSizeColor> ProductSizeColors { get; set; } = new List<ProductSizeColor>();
    }
}