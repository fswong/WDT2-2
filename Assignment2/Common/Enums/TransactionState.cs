using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Common.Enums
{
    public enum TransactionState
    {
        open = 0,
        committed = 1,
        rolledback = 2
    }
}
