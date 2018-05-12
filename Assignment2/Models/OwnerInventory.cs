using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class OwnerInventory
    {
        [Key]
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int StockLevel { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }
    }
}
