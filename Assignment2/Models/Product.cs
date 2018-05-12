using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2.Models.DataModel
{
    [DataContract]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.StockRequests = new HashSet<StockRequest>();
            this.StoreInventories = new HashSet<StoreInventory>();
        }

        [Key]
        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public virtual OwnerInventory OwnerInventory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [DataMember]
        public virtual ICollection<StockRequest> StockRequests { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        [DataMember]
        public virtual ICollection<StoreInventory> StoreInventories { get; set; }
    }
}
