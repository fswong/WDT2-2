using Assignment2.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class StoreInventoryViewModel
    {
        public Store store { get; set; }

        public List<StoreInventory> items { get; set; }
    }
}
