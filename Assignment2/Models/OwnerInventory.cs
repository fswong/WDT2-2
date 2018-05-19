using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class OwnerInventory
    {
        [Key, ForeignKey("Product"), Display(Name = "Product ID")]
        [DataMember]
        public int ProductID { get; set; }

        [Display(Name = "Stock Level")]
        [DataMember]
        public int StockLevel { get; set; }

        [DataMember]
        public virtual Product Product { get; set; }
    }
}
