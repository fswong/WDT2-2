using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Assignment2.Common.Enums;

namespace Assignment2.Helpers
{
    /// <summary>
    /// from tutor;s code
    /// </summary>
    public class CreditCardHelper
    {
        #region properties
        public string RegEx { get; set; }
        public int Length { get; set; }
        public CardType Type { get; set; }
        #endregion

        #region ctor
        public CreditCardHelper(string regEx, int length, CardType type)
        {
            RegEx = regEx;
            Length = length;
            Type = type;
        }
        #endregion

        /// <summary>
        /// list of possible matches
        /// regex
        /// length
        /// type
        /// </summary>
        private readonly static CreditCardHelper[] cards =
        {
            new CreditCardHelper("^(51|52|53|54|55)", 16, CardType.MasterCard),
            new CreditCardHelper("^(4)", 16, CardType.VISA),
            new CreditCardHelper("^(4)", 13, CardType.VISA),
            new CreditCardHelper("^(34|37)", 15, CardType.Amex)
        };

        /// <summary>
        /// match card if valid
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static CardType GetCardType(string cardNumber)
        {
            if (cardNumber != null)
            {
                foreach (CreditCardHelper card in cards)
                {
                    if (cardNumber.Length == card.Length &&
                       Regex.IsMatch(cardNumber, card.RegEx))
                    {
                        return card.Type;
                    }
                }
            }

            return CardType.Unknown;
        }
    }
}
