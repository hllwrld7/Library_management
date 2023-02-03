using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Objects
{
    public class BaseReceipt
    {
        public DateTime DueDate { get => DateTime.Now.AddMonths(1) ; }
    }
}
