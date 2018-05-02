using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    public partial class StockRequest
    {
        public int StockRequestID { get; set; }
        public int StoreID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public virtual Product Product { get; set; }
        public virtual Store Store { get; set; }
    }
}
