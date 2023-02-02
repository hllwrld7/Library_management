using ClientWPF.Core.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClientWPF.Core.Books
{
    public enum FictionType
    {
        None,
        Drama,
        FairyTale,
        Comedy
    }

    public class Fiction : IBook
    {
        private string _title;
        private string _author;
        private FictionType _type;
        private bool _isBorrowed;

        public string Title => _title;

        public string Author => _author;

        public Enum Type => _type;
        public bool IsBorrowed { get => _isBorrowed; set => _isBorrowed = value; }
        public Fiction(string title, string author, string type, bool isBorrowed)
        {
            _author = author;
            _title = title;
            _type = (FictionType)SetType(type);
            _isBorrowed = isBorrowed;
        }
        public Fiction(string title, string author, FictionType type, bool isBorrowed)
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
                case (FictionType.Drama): return "Drama";
                case (FictionType.FairyTale): return "FairyTale";
                case (FictionType.Comedy): return "Comedy";
                default: return "None";
            }
        }

        public Enum SetType(string category)
        {
            if (category == "Drama")
                return FictionType.Drama;
            if (category == "FairyTale")
                return FictionType.FairyTale;
            if (category == "Comedy")
                return FictionType.Comedy;
            return EncyclopediaType.None;
        }

        public string[] ToStringArray()
        {
            return new[] { _title, _author, _type.ToString(), _isBorrowed ? "true" : "false" };
        }
    }
}
