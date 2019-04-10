using System.Collections.Generic;

namespace ProjectWombat.Models {
    public class Order {
        public Order() {
            Rows = new List<OrderRow>();
            Status = OrderStatus.Preparing;
        }

        public string Id { get; set; }
        public decimal Amount { get; set; }
        public List<OrderRow> Rows { get; set; }
        public OrderStatus Status { get; set; }
    }
}
