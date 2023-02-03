using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Contracts.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Objects
{
    public class Order: BaseReceipt
    {
        private Dictionary<IBook, BookCategory> _books;

        public Dictionary<IBook, BookCategory> Books { get { return _books; } }
        public Order() 
        {
            _books = new Dictionary<IBook, BookCategory>();
        }

        public void AddBook(IBook book, BookCategory category)
        {
            _books[book] = category;
        }

        public void RemoveBook(IBook book)
        {
            _books.Remove(book);
        }

        public override string ToString()
        {
            StringBuilder orderString= new StringBuilder();
            orderString.AppendLine("Your order:");
            orderString.AppendLine("**********************");
            foreach(IBook book in _books.Keys) 
                orderString.AppendLine(book.ToString());
            orderString.AppendLine("**********************");
            orderString.AppendLine($"Due date: {DueDate:dddd, dd MMMM yyyy}");
            orderString.AppendLine("**********************");
            return orderString.ToString();
        }
    }
}
