using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2WebAPI.Extensions
{
    public static class StoreInventoryExtension
    {
        /// <summary>
        /// converts from datamodel to restobject
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static REST.StoreInventory ToRest(this Assignment2.Models.DataModel.StoreInventory item) {
            return new REST.StoreInventory {
                StoreID = item.StoreID,
                StoreName = item.Store.Name,
                ProductID = item.ProductID,
                ProductName = item.Product.Name,
                Price = item.Product.Price,
                StockLevel = item.StockLevel
            };
        }
    }
}
