using System.Data.SQLite;
using System.IO;
using System.Text;
using ClientWPF.Core.Books;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Models;
using ClientWPF.Core.Objects;
using Newtonsoft.Json;

namespace ClientWPF.Core.Services;

public class SqliteService : ISqliteService
{
    SQLiteConnection _dbConnection;
    
    public SqliteService()
    {
        _dbConnection = new SQLiteConnection("Data Source=LibraryDb.sqlite;");
        if (_dbConnection == null)
            SQLiteConnection.CreateFile("LibraryDb.sqlite");
        _dbConnection.Open();
        ExecuteDbQuery("CreateDbsIfNotExist", null, BookCategory.None);
    }

    public void WriteToDb(Catalogue catalogue, BookCategory category)
    {
        foreach(IBook book in catalogue)
            ExecuteDbQuery("WriteToDb", book.ToStringArray(), category );
    }

    public Catalogue ReadFromDbQuery(BookCategory category)
    {
        var tableName = GetBookCategoryString(category);
        string query = $"SELECT * FROM {tableName};";
        SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
        var reader = command.ExecuteReader();
        if (reader == null)
            return null;
        var data = new Catalogue();
        while (reader.Read())
        {
            var isBorrowed = reader[3].ToString() == "true" ? true : false;
            data.Add(ConvertToBook(category, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), isBorrowed));
        }
            
            
        return data;
    }

    private string GetBookCategoryString(BookCategory category)
    {
        switch(category) 
        {
            case (BookCategory.Dictionary): return "dictionaries";
            case (BookCategory.Fiction): return "fiction";
            case (BookCategory.Encyclopedia): return "encyclopedias"; 
        }
        return "other";
    }

    private IBook ConvertToBook(BookCategory category, string title, string author, string type, bool isBorrowed)
    {
        IBook book = null;
        switch (category)
        {
            case (BookCategory.Dictionary): return new Dictionary(title, author, type, isBorrowed);
            case (BookCategory.Fiction): return new Fiction(title, author, type, isBorrowed);
            case (BookCategory.Encyclopedia): return new Encyclopedia(title, author, type, isBorrowed);
        }
        return new Other(title, author, isBorrowed);
    }

    public void DeleteFromDb(Dictionary book)
    {
        //ExecuteDbQuery("DeleteFromDb", new string[4] { book.Id.ToString(), book.Title, book.DueDate, book.Category.ToString() });
    }

    private void ExecuteDbQuery(string queryType, string[] args, BookCategory category)
    {
        string query = String.Empty;
        if (queryType == "CreateDbsIfNotExist")
            query = "CREATE TABLE IF NOT EXISTS dictionaries (title TEXT, author TEXT, category TEXT, borrowed BOOL);" +
                "CREATE TABLE IF NOT EXISTS fiction (title TEXT, author TEXT, category TEXT, borrowed BOOL);" +
                "CREATE TABLE IF NOT EXISTS encyclopedias (title TEXT, author TEXT, category TEXT, borrowed BOOL);" +
                "CREATE TABLE IF NOT EXISTS other (title TEXT, author TEXT, category TEXT, borrowed BOOL);";
        else if (queryType == "WriteToDb")
            query = $"INSERT INTO {GetBookCategoryString(category)} (title, author, category, borrowed) values ('{args[0]}', '{args[1]}', '{args[2]}', {args[3]});";
        else if (queryType == "DeleteFromDb")
            query = $"DELETE FROM catalogue WHERE AND title = '{args[0]}' AND author = '{args[1]}' AND category = '{args[2]}');";
        SQLiteCommand command = new SQLiteCommand(query, _dbConnection);
        command.ExecuteNonQuery();
    }

    public void Dispose()
    {
        _dbConnection.Close();
    }

    List<IBook> ISqliteService.ReadFromDbQuery()
    {
        throw new NotImplementedException();
    }

    public void DeleteFromDb(IBook book)
    {
        throw new NotImplementedException();
    }
}
