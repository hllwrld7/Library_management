using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Models;
namespace ClientWPF.Core.Contracts.Services;

public interface ISqliteService: IDisposable
{
    public void WriteToDb(Catalogue catalogue, BookCategory category);

    public List<IBook> ReadFromDbQuery();

    public void DeleteFromDb(IBook book);
}
