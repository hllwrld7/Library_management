using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Models;
using ClientWPF.Core.Objects;

namespace ClientWPF.Core.Services;

public class LibraryManagementService : ILibraryManagementService
{
    SqliteService _sqliteService;
    Catalogue _dictionaries = new Catalogue();
    Catalogue _encyclopedias = new Catalogue();
    Catalogue _fiction = new Catalogue();
    Catalogue _otherBooks = new Catalogue();

    Order _currentOrder;

    public LibraryManagementService()
    {
        _sqliteService = new SqliteService();

    }

    public event EventHandler<int> OrderBookNumberChanged;


    private void OnOrderChanged(int e)
    {
        OrderBookNumberChanged?.Invoke(this, e);
    }

    public void AddToOrder(IBook book, BookCategory category)
    {
        if(_currentOrder == null)
            _currentOrder= new Order();
        
        _currentOrder.AddBook(book, category);
        UpdateCategoryCatalogue(book, category, true);
        OnOrderChanged(_currentOrder.Books.Count);
    }

    private void UpdateCategoryCatalogue(IBook book, BookCategory category, bool isBorrowed)
    {
        switch (category)
        {
            case (BookCategory.Dictionary): _dictionaries.UpdateIsBorrowed(book, isBorrowed); break;
            case (BookCategory.Encyclopedia): _encyclopedias.UpdateIsBorrowed(book, isBorrowed); break;
            case (BookCategory.Fiction): _fiction.UpdateIsBorrowed(book, isBorrowed); break;
            case(BookCategory.None): _otherBooks.UpdateIsBorrowed(book, isBorrowed); break;
        }
    }

    public void DisposeOfSqliteService()
    {
        _sqliteService.Dispose();
    }

    public void FillLibrary()
    {
        EmptyCatalogues();
        _dictionaries.Add(new Dictionary("English Grammar", "Raymond Murray", DictType.Grammar, false));
        _dictionaries.Add(new Dictionary("Polish-English dictionary", " Joanna Michalak-Gray", DictType.Translation, false));
        _dictionaries.Add(new Dictionary("Polish dictionary", " Joanna Michalak-Gray", DictType.Definitions, false));

        _encyclopedias.Add(new Encyclopedia("C# for dummies", "Andrew Stellman", EncyclopediaType.Computer_Science, false));
        _encyclopedias.Add(new Encyclopedia("Biology encyclopedia", "Don Rittner", EncyclopediaType.Natural_Sciences, false));
        
        _fiction.Add(new Fiction("A question of guilt", "Jorn Lien Hostmann", FictionType.Drama, false));
        _fiction.Add(new Fiction("It ends with us", "Colleen Hoover", FictionType.Drama, false));
        _fiction.Add(new Fiction("Polish fairy tales", "Various", FictionType.FairyTale, false));

        _otherBooks.Add(new Other("Jojos Bizzare Adventure", "Hirohiko Araki", false));
    }

    private void EmptyCatalogues()
    {
        _dictionaries.Clear();
        _encyclopedias.Clear();
        _fiction.Clear();
        _otherBooks.Clear();
    }

    public Order GetCurrentOrder()
    {
        if(_currentOrder==null)
            _currentOrder= new Order();
        return _currentOrder;
    }

    public async Task<Catalogue> GetGridDataAsync(BookCategory category)
    {
        await Task.CompletedTask;
        return GetCategoryCatalogue(category);
    }

    public void RemoveFromOrder(IBook book, BookCategory category)
    {
        if(_currentOrder == null) return;
        _currentOrder.RemoveBook(book);
        UpdateCategoryCatalogue(book, category, false);
        OnOrderChanged(_currentOrder.Books.Count);
    }

    private Catalogue GetCategoryCatalogue(BookCategory category)
    {
        switch (category)
        {
            case (BookCategory.Dictionary): return _dictionaries;
            case(BookCategory.Encyclopedia): return _encyclopedias;
            case(BookCategory.Fiction): return _fiction;
            default: return _otherBooks;
        }
    }

    public void FinishOrder()
    {
        if(_currentOrder != null)
            foreach(var book in _currentOrder.Books.Keys) 
                Receipt.CreateReceipt(book, _currentOrder.DueDate);
        _currentOrder = null;
        OnOrderChanged(0);
    }

    public void RewriteDbTables()
    {
        if(_currentOrder != null)
        {
            foreach(var book in _currentOrder.Books.Keys)
                RemoveFromOrder(book, _currentOrder.Books[book]);
        }
        _sqliteService.RecreateTables();
        _sqliteService.WriteToDb(_dictionaries, BookCategory.Dictionary);
        _sqliteService.WriteToDb(_encyclopedias, BookCategory.Encyclopedia);
        _sqliteService.WriteToDb(_fiction, BookCategory.Fiction);
        _sqliteService.WriteToDb(_otherBooks, BookCategory.None);
    }

    public void RestoreData()
    {
        _dictionaries = _sqliteService.ReadFromDbQuery(BookCategory.Dictionary);
        _encyclopedias = _sqliteService.ReadFromDbQuery(BookCategory.Encyclopedia);
        _fiction = _sqliteService.ReadFromDbQuery(BookCategory.Fiction);
        _otherBooks = _sqliteService.ReadFromDbQuery(BookCategory.None);
    }
}
