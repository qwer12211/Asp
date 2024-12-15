using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class ProductCatalog
    {
        public ProductCatalog()
        {
            CartItems = new HashSet<CartItem>();
            PosOrders = new HashSet<PosOrder>();
        }

        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Images { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? CategoryId { get; set; }
        public string? CatalogDescription { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<PosOrder> PosOrders { get; set; }
    }
}
