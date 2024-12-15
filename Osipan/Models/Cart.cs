using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class Cart
    {
        public int IdCart { get; set; }
        public int? CatalogId { get; set; }
        public int? UsersId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Catalog? Catalog { get; set; }
        public virtual User? Users { get; set; }
    }
}
