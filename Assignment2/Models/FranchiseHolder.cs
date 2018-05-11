using Assignment2.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models
{
    public class FranchiseHolder
    {
        int StoreID { get; set; }
        string Id { get; set; } // this is the user id

        public virtual Store Store { get; set; }
    }
}
