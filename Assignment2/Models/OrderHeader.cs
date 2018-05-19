using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    [DataContract]
    public class OrderHeader
    {
        [DataMember]
        [Key]
        public string OrderMain { get; set; }

        [DataMember]
        public virtual ICollection<Order> StockRequests { get; set; }
    }
}
