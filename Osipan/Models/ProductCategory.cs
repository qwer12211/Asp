using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Catalogs = new HashSet<Catalog>();
        }

        public int IdCategory { get; set; }
        public string? CategoryName { get; set; }

        public virtual ICollection<Catalog> Catalogs { get; set; }
    }
}
