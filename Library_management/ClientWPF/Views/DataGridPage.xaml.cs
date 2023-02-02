using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Markup;
using ClientWPF.Contracts.Views;
using ClientWPF.Core.Contracts.Interfaces;
using ClientWPF.Core.Contracts.Services;
using ClientWPF.Core.Models;

namespace ClientWPF.Views;

public partial class DataGridPage : Page, INotifyPropertyChanged, INavigationAware
{
    private readonly ILibraryManagementService _sampleDataService;

    public ObservableCollection<IBook> Source { get; } = new ObservableCollection<IBook>();

    public DataGridPage(ILibraryManagementService sampleDataService)
    {
        _sampleDataService = sampleDataService;
        InitializeComponent();
        DataContext = this;
    }

    public async void OnNavigatedTo(object parameter)
    {
        Source.Clear();
        var category = BookCategory.None;
        if(parameter != null)
        {
            if(parameter is BookCategory)
                category = (BookCategory)parameter;               
        }
        var data = await _sampleDataService.GetGridDataAsync(category);
        foreach (var item in data)
        {
            Source.Add((IBook)item);
        }
    }

    public void OnNavigatedFrom()
    {
        
    }

    public event PropertyChangedEventHandler PropertyChanged;
    public event EventHandler BookChosen;

    private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (Equals(storage, value))
        {
            return;
        }

        storage = value;
        OnPropertyChanged(propertyName);
    }

    private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private void OnBookChosen(object sender, EventArgs e) 
    {
        BookChosen?.Invoke(this, e);
    }
    private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        _sampleDataService.BorrowBook((IBook)dgBooks.SelectedItem);
        OnBookChosen(sender, e);
    }
}
