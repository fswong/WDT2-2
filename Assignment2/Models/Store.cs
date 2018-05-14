using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class Store
    {
        public Store()
        {
            this.StockRequests = new HashSet<StockRequest>();
            this.StoreInventories = new HashSet<StoreInventory>();
        }

        [Key]
        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public virtual ICollection<StockRequest> StockRequests { get; set; }

        [DataMember]
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
