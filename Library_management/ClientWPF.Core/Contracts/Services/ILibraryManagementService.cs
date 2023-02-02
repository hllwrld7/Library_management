using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Models;
using ClientWPF.Core.Services;

namespace ClientWPF.Core.Contracts.Services;

public enum BookCategory
{
    None,
    Dictionary,
    Encyclopedia,
    Fiction
}

public interface ILibraryManagementService
{
    public void AddBook(string title, string dueDate, string category);

    public void DisposeOfSqliteService();
    public void BorrowBook(IBook book);
    Task<Catalogue> GetGridDataAsync(BookCategory category);
    public void FillLibrary();
}
