using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Objects;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Runtime.CompilerServices;

namespace ClientWPF.Core.Models;

public class Catalogue : IList<IBook>, IEnumerable
{
    private List<IBook> _catalogue;

    public bool IsFixedSize => false;

    public bool IsReadOnly => false;

    public int Count => _catalogue.Count;

    public bool IsSynchronized => true;

    public object SyncRoot => new object();

    IBook IList<IBook>.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public object this[int index] { get => _catalogue[index]; set => _catalogue[index] = (IBook)value; }

    public Catalogue()
    {
        _catalogue = new List<IBook>();
    }

    public Catalogue(List<IBook> catalogue) 
    {
        _catalogue = catalogue;
    }

    public void Clear()
    {
        _catalogue.Clear();
    }

    public bool Contains(object value)
    {
        return _catalogue.Contains((IBook)value);
    }

    public int IndexOf(object value)
    {
        return _catalogue.IndexOf((IBook)value);
    }

    public void Insert(int index, object value)
    {
        _catalogue.Insert(index, (IBook)value);
    }

    public void Remove(object value)
    {
        _catalogue.Remove((IBook)value);
    }

    public void RemoveAt(int index)
    {
        _catalogue.RemoveAt(index);
    }

    public void CopyTo(Array array, int index)
    {
        _catalogue.CopyTo((Dictionary[])array);
    }

    public IEnumerator GetEnumerator()
    {
        return _catalogue.GetEnumerator();
    }

    public int IndexOf(IBook item)
    {
        return _catalogue.IndexOf(item);
    }

    public void Insert(int index, IBook item)
    {
        _catalogue.Insert(index, item);
    }

    public void Add(IBook item)
    {
        _catalogue.Add(item);
    }

    public bool Contains(IBook item)
    {
        return _catalogue.Contains(item);
    }

    public void CopyTo(IBook[] array, int arrayIndex)
    {
        _catalogue.CopyTo(array, arrayIndex);
    }

    public bool Remove(IBook item)
    {
        return _catalogue!= null && _catalogue.Remove(item);
    }

    IEnumerator<IBook> IEnumerable<IBook>.GetEnumerator()
    {
        return _catalogue.GetEnumerator();
    }

    public void UpdateIsBorrowed(IBook book, bool value)
    {
        var obj = _catalogue.FirstOrDefault(x => x == book);
        if (obj != null) obj.IsBorrowed = value;
    }
}
