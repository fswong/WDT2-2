using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models.OwnerViewModels
{
    public class OwnerInventoryViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessageResourceName = "The Product Id is required")]
        public int ProductID { get; set; }

        [Display(Name = "Product")]
        public int StockLevel { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessageResourceName = "The quantity is required")]
        public string ProductName { get; set; }
    }
}
