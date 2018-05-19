using Assignment2.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2WebAPI.REST
{
    [DataContract]
    public class RESTOrder : IHttpResponse, IRESTObject
    {
        [DataMember]
        public int OrderID { get; set; }

        [DataMember]
        public string CustomerID { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DisplayName("Product Name")]
        [DataMember]
        public string ProductName { get; set; }

        [DisplayName("Store Name")]
        [DataMember]
        public string StoreName { get; set; }

        [DisplayName("Price")]
        [DataMember]
        [DataType(DataType.Currency)]
        public double? ProductPrice { get; set; }

        public RESTOrder() { }
    }
}
