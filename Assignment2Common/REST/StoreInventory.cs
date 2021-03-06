﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Assignment2WebAPI.REST
{
    [DataContract]
    public class StoreInventory
    {
        [DataMember]
        public int StoreID { get; set; }

        [DataMember]
        public string StoreName { get; set; }

        [DataMember]
        public int ProductID { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public double Price { get; set; }

        [DataMember]
        public int StockLevel { get; set; }
    }
}
