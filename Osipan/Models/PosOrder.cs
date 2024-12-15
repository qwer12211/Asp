using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class PosOrder
    {
        public int IdPosOrder { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Catalog? Product { get; set; }
    }
}
