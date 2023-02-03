using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Objects;
using System;

namespace ClientWPF.Core.Books;

public enum DictType
{
    None,
    Definitions,
    Translation,
    Grammar,
}

public class Dictionary : IBook
{
    private string _title;
    private string _author;
    private DictType _type;
    private bool _isBorrowed;

    public string Title => _title;

    public string Author => _author;

    public Enum Type => _type;

    public bool IsBorrowed { get => _isBorrowed; set => _isBorrowed = value; }

    public Dictionary(string title, string author, string type, bool isBorrowed)
    {
        _author = author;
        _title = title;
        _type = (DictType)SetType(type);
        _isBorrowed = isBorrowed;
    }

    public Dictionary(string title, string author, DictType type, bool isBorrowed)
    {
        _author = author;
        _title = title;
        _type = type;
        _isBorrowed = isBorrowed;
    }

    public string GetCategoryString()
    {
        switch(_type)
        {
            case (DictType.Definitions): return "Definitions";
            case (DictType.Translation): return "Translation";
            case (DictType.Grammar): return "Grammar";
            default: return "None";
        }
    }

    public Enum SetType(string category)
    {
        if (category == "Definitions")
            return DictType.Definitions;
        if (category == "Translation")
            return DictType.Translation;
        if (category == "Grammar")
            return DictType.Grammar;
        return DictType.None;
    }

    public string[] ToStringArray()
    {
        return new[] { _title, _author, _type.ToString(), _isBorrowed ? "true" : "false" };
    }

    public override string ToString()
    {
        return $"{_title} - {_author} - {_type}";
    }
}
