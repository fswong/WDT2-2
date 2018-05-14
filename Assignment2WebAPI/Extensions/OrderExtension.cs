using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2WebAPI.Extensions
{
    public static class OrderExtension
    {
        /// <summary>
        /// changes data object to rest object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static REST.Order ToRest(this Assignment2.Models.Order item){
            return new REST.Order{
                OrderID = item.OrderID,
                CustomerID = item.CustomerID,
                ProductID = item.ProductID,
                ProductName = item.Product.Name,
                StoreID = item.StoreID,
                StoreName = item.Store.Name,
                Quantity = item.Quantity,
                ProductPrice = item.Product.Price
            };
        }
    }
}
