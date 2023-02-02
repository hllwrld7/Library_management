using ClientWPF.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Books
{
    public enum EncyclopediaType
    {
        None,
        Natural_Sciences,
        Linguistics,
        Computer_Science
    }

    public class Encyclopedia : IBook
    {
        private string _title;
        private string _author;
        private EncyclopediaType _type;
        private bool _isBorrowed;

        public string Title => _title;

        public string Author => _author;

        public Enum Type => _type;
        public bool IsBorrowed { get => _isBorrowed; set => _isBorrowed = value; }

        public Encyclopedia(string title, string author, string type, bool isBorrowed)
        {
            _author = author;
            _title = title;
            _type = (EncyclopediaType)SetType(type);
            _isBorrowed = isBorrowed;
        }
        public Encyclopedia(string title, string author, EncyclopediaType type, bool isBorrowed)
        {
            _author = author;
            _title = title;
            _type = type;
            _isBorrowed= isBorrowed;
        }

        public string GetCategoryString()
        {
            switch (_type)
            {
                case (EncyclopediaType.Natural_Sciences): return "Natural Sciences";
                case (EncyclopediaType.Linguistics): return "Linguistics";
                case (EncyclopediaType.Computer_Science): return "Computer Science";
                default: return "None";
            }
        }

        public Enum SetType(string category)
        {
            if (category == "Defintions")
                return EncyclopediaType.Natural_Sciences;
            if (category == "Translation")
                return EncyclopediaType.Linguistics;
            if (category == "Grammar")
                return EncyclopediaType.Computer_Science;
            return EncyclopediaType.None;
        }

        public string[] ToStringArray()
        {
            return new[] { _title, _author, _type.ToString(), _isBorrowed ? "true" : "false" };
        }
    }
}
