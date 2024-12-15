using System;
using System.Collections.Generic;

namespace Osipan.Models
{
    public partial class Order
    {
        public Order()
        {
            PosOrders = new HashSet<PosOrder>();
        }

        public int IdOrder { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? UserId { get; set; }
        public decimal? TotalSum { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<PosOrder> PosOrders { get; set; }
    }
}
