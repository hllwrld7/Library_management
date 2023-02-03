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
    private BookCategory _category;

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
        _category = BookCategory.None;
        if(parameter != null)
        {
            if(parameter is BookCategory)
                _category = (BookCategory)parameter;               
        }
        var data = await _sampleDataService.GetGridDataAsync(_category);
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
        _sampleDataService.AddToOrder((IBook)dgBooks.SelectedItem, _category);
        dgBooks.Items.Refresh();
    }

    private void ContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
    {
        SetButtonState();
    }

    private void SetButtonState()
    {
        if (dgBooks.SelectedItem != null)
        {
            var selectedBook = (IBook)dgBooks.SelectedItem;
            var order = _sampleDataService.GetCurrentOrder(); 
            btBorrowBook.IsEnabled = true;
            btRemoveFromOrder.IsEnabled = false;
            if (selectedBook != null && selectedBook.IsBorrowed)
            {
                btBorrowBook.IsEnabled = false;
                if (order.Books.Keys.Contains(selectedBook))
                    btRemoveFromOrder.IsEnabled = true;
            }
        }
    }

    private void MenuItem_Click_1(object sender, System.Windows.RoutedEventArgs e)
    {
        _sampleDataService.RemoveFromOrder((IBook)dgBooks.SelectedItem, _category);
        dgBooks.Items.Refresh();
    }
}
