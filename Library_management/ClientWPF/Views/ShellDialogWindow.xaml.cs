using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

using ClientWPF.Contracts.Views;
using ClientWPF.Core.Contracts.Services;
using MahApps.Metro.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace ClientWPF.Views;

public partial class ShellDialogWindow : Page, INavigationAware
{
    ILibraryManagementService _libraryManagementService;
    public ShellDialogWindow(ILibraryManagementService sampleDataService)
    {
        InitializeComponent();
        DataContext = this;
        _libraryManagementService = sampleDataService;
    }

    public void OnNavigatedFrom()
    {
        
    }

    public void OnNavigatedTo(object parameter)
    {
        tbReceipt.Text = _libraryManagementService.GetCurrentOrder().ToString();
    }

    private void CheckOutClick(object sender, RoutedEventArgs e)
    {
        _libraryManagementService.FinishOrder();
        tbReceipt.Text = $"The books are borrowed! \n The receipts are stored in {AppDomain.CurrentDomain.BaseDirectory}.";
    }
}
