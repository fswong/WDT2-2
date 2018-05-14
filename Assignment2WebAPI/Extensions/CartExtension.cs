using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2WebAPI.Extensions
{
    public static class CartExtension
    {
        /// <summary>
        /// changes a data object to rest object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static REST.Cart ToRest(this Assignment2.Models.Cart item)
        {
            return new REST.Cart
            {
                CustomerID = item.CustomerID,
                ProductID = item.ProductID,
                ProductName = item.Product.Name,
                StoreID = item.StoreID,
                StoreName = item.Store.Name,
                Quantity = item.Quantity,
                ProductPrice = item.Product.Price
            };
        }

        /// <summary>
        /// changes rest object to data object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static Assignment2.Models.Cart ToDataModel(this REST.Cart item) {
            return new Assignment2.Models.Cart {
                CustomerID = item.CustomerID,
                ProductID = item.ProductID,
                StoreID = item.StoreID,
            };
        }
    }
}
