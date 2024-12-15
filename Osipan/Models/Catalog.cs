using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class Catalog
    {
      

        public int IdCatalog { get; set; }
        public string Names { get; set; } = null!;
        public string? Descriptions { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string? CatalogDescription { get; set; }
        public int? CategoryId { get; set; }

        public virtual ProductCategory? Category { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<PosOrder> PosOrders { get; set; }
    }
}
