namespace selemstore_test.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerAddress { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
