using ClientWPF.Core.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Objects
{
    internal class Order
    {
        private int _id;
        private string _customerName;
        private Dictionary _borrowedBook;
        private DateTime _orderDate;
    }
}
