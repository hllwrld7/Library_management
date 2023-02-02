using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Contracts.Interfaces
{
    public interface IBook
    {
        public string Title { get; }
        public string Author { get; }
        public Enum Type { get; }

        public bool IsBorrowed { get; set; }

        public string ToString();
        public Enum SetType(string category);
        public string[] ToStringArray();
        public string GetCategoryString();
    }
}
