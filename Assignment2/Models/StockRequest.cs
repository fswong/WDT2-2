using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class StockRequest
    {
        [Display(Name = "Stock Request ID")]
        [DataMember]
        public int StockRequestID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }

        [DataMember]
        public virtual Store Store { get; set; }
    }
}
