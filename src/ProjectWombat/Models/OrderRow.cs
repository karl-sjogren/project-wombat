
using System;

namespace ProjectWombat.Models {
    public class OrderRow {
        public Product Product { get; set; }
        public Int32 Count { get; set; }
        public decimal Total => Count * Product.Cost;    
    }
}
