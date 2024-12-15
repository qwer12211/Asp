using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class CartItem
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public int? Quantity { get; set; }

        public virtual ProductCatalog Product { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
