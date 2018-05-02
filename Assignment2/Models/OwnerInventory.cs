using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    public partial class OwnerInventory
    {
        [Key]
        public int ProductID { get; set; }
        public int StockLevel { get; set; }

        public virtual Product Product { get; set; }
    }
}
