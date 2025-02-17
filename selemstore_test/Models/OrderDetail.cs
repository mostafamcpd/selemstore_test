namespace selemstore_test.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }

        public Product Product { get; set; }
        public Order Order { get; set; }
    }

}
