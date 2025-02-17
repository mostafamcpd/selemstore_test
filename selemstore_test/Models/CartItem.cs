using System.ComponentModel.DataAnnotations;

namespace selemstore_test.Models
{
    public class CartItem
    {
        [Key]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public int SizeId { get; set; }
        public string Size { get; set; }

        public int ColorId { get; set; }
        public string Color { get; set; }
    }
}

//using System.ComponentModel.DataAnnotations;

//namespace selemstore_test.Models
//{
//    public class CartItem
//    {
//        [Key]
//        public int ProductId { get; set; }
//        public Product Product { get; set; }
//        public int Quantity { get; set; }
//        public int SizeId {  get; set; }
//        public string Size { get; set; }
//        public int ColorId { get; set; }
//        public string Color { get; set; }


//    }
//}
