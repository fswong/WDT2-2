using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class StockRequest
    {
        [DataMember]
        public int StockRequestID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }

        [DataMember]
        public virtual Store Store { get; set; }
    }
}
