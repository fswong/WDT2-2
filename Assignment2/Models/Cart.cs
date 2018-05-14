using Assignment2.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    [DataContract]
    public class Cart
    {
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        public Product Product { get; set; }

        public Store Store { get; set; }
    }
}
