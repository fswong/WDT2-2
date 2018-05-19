using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Assignment2.Common.Enums;

namespace Assignment2.Models
{
    public class CreditCardViewModel
    {
        [Display(Name = "Credit Card Type")]
        [Required]
        public CardType CreditCardType { get; set; }

        [Display(Name = "Credit Card Number")]
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }
    }
}
