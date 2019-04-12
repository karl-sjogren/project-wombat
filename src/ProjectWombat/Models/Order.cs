using System.Collections.Generic;
using System.Linq;

namespace ProjectWombat.Models {
    public class Order {
        public Order() {
            Rows = new List<OrderRow>();
            Status = OrderStatus.Preparing;
        }

        public decimal Amount => Rows.Sum(row => row.Total);

        public string Id { get; set; }
        public List<OrderRow> Rows { get; set; }
        public OrderStatus Status { get; set; }
    }
}
