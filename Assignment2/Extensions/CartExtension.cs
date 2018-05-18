using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public static REST.RESTCart ToRest(this Assignment2.Models.Cart item)
        {
            return new REST.RESTCart
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
        public static Assignment2.Models.Cart ToDataModel(this REST.RESTCart item) {
            return new Assignment2.Models.Cart {
                CustomerID = item.CustomerID,
                ProductID = item.ProductID,
                StoreID = item.StoreID,
                Quantity = item.Quantity
            };
        }

        public static FormUrlEncodedContent ToHttpBody(this REST.RESTCart item) {
            var pairs = new List<KeyValuePair<string, string>>();
            
            pairs.Add(new KeyValuePair<string, string>("CustomerID", item.CustomerID));
            pairs.Add(new KeyValuePair<string, string>("ProductID", item.ProductID.ToString()));
            pairs.Add(new KeyValuePair<string, string>("StoreID", item.StoreID.ToString()));
            pairs.Add(new KeyValuePair<string, string>("Quantity", item.Quantity.ToString()));

            var content = new FormUrlEncodedContent(pairs);

            return content;
        }
    }
}
