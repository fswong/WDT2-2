using Assignment2.Data;
using Assignment2.Models.DataModel;
using Assignment2.Models.OwnerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Extensions.DataExtensions
{
    public static class OwnerInventoryExtension
    {
        public static OwnerInventoryViewModel ToViewModel(this OwnerInventory obj, ApplicationDbContext context) {
            try {
                var product = context.Products.Where(p => p.ProductID == obj.ProductID).First();

                var response = new OwnerInventoryViewModel()
                {
                    ProductID = obj.ProductID,
                    StockLevel = obj.StockLevel,
                    ProductName = product.Name
                };

                return response;
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}
