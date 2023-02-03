using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Models;
using ClientWPF.Core.Objects;
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
    public void DisposeOfSqliteService();
    public void AddToOrder(IBook book, BookCategory category);

    public void RemoveFromOrder(IBook book, BookCategory category);
    public Order GetCurrentOrder();
    Task<Catalogue> GetGridDataAsync(BookCategory category);
    public void FillLibrary();

    public event EventHandler<int> OrderBookNumberChanged;

    public void FinishOrder();
    public void RewriteDbTables();

    public void RestoreData();
}
