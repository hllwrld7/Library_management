using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Models;

namespace ClientWPF.Core.Services;

public class LibraryManagementService : ILibraryManagementService
{
    SqliteService _sqliteService;
    Catalogue _dictionaries = new Catalogue();
    Catalogue _encyclopedias = new Catalogue();
    Catalogue _fiction = new Catalogue();
    Catalogue _otherBooks = new Catalogue();

    public LibraryManagementService()
    {

        _sqliteService = new SqliteService();
        FillLibrary();
        _sqliteService.WriteToDb(_dictionaries, BookCategory.Dictionary);
        _sqliteService.WriteToDb(_encyclopedias, BookCategory.Encyclopedia);
        _sqliteService.WriteToDb(_fiction, BookCategory.Fiction);
        _sqliteService.WriteToDb(_otherBooks, BookCategory.None);
        _dictionaries = _sqliteService.ReadFromDbQuery(BookCategory.Dictionary);
        _encyclopedias = _sqliteService.ReadFromDbQuery(BookCategory.Encyclopedia);
        _fiction = _sqliteService.ReadFromDbQuery(BookCategory.Fiction);
        _otherBooks = _sqliteService.ReadFromDbQuery(BookCategory.None);
    }

    public void AddBook(string title, string dueDate, string category)
    {
        
    }

    public void BorrowBook(IBook book)
    {
        book.IsBorrowed = true;
    }

    public void DisposeOfSqliteService()
    {
        _sqliteService.Dispose();
    }

    public void FillLibrary()
    {
        _dictionaries.Add(new Dictionary("Book 1", "Author 1", DictType.Grammar, false));
        _encyclopedias.Add(new Encyclopedia("Book 2", "Author 2", EncyclopediaType.Natural_Sciences, false));
        _fiction.Add(new Fiction("Book 3", "Author 3", FictionType.Drama, false));
        _otherBooks.Add(new Other("Book 4", "Author 4", false));
    }

    public async Task<Catalogue> GetGridDataAsync(BookCategory category)
    {
        await Task.CompletedTask;
        var catalogue = GetCategoryCatalogue(category).ToList();
        var availableBooks = catalogue.Where(v => !v.IsBorrowed);
        return new Catalogue(availableBooks.ToList());
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
}
