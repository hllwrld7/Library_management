using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Objects
{
    public static class Receipt
    {
        public static void CreateReceipt(IBook book, DateTime dueDate) 
        {
            var path = $"{book.Title}_Receipt.txt";
            string[] contents =
            {
                "RECEIPT",
                "**********",
                $"{book.ToString()}",
                String.Empty,
                $"Order date: {DateTime.Now:dddd, dd MMMM yyyy}",
                $"Due date: {dueDate:dddd, dd MMMM yyyy}",
                "**********"
            };
            File.WriteAllLines(path, contents);
        }
    }
}
