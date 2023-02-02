using ClientWPF.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ClientWPF.Core.Books
{
    public class Other : IBook
    {
        private string _title;
        private string _author;
        private bool _isBorrowed;
        public string Title => _title;

        public string Author => _author;

        public Enum Type => null;

        public bool IsBorrowed { get => _isBorrowed; set => _isBorrowed = value; }

        public Other(string title, string author, bool isBorrowed)
        {
            _author = author;
            _title = title;
            _isBorrowed = isBorrowed;
        }

        public string GetCategoryString()
        {
            return String.Empty;
        }

        public Enum SetType(string category)
        {
            return null;
        }

        public string[] ToStringArray()
        {
            return new[] { _title, _author, string.Empty, _isBorrowed ? "true" : "false" };
        }
    }
}
