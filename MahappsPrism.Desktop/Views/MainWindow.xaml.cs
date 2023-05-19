using System;
using MahappsPrism.Desktop.ViewModels;

namespace MahappsPrism.Desktop.Views;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }
    
    //Uygulama açılıp, ekranda gözüktükten sonra ilk bu method çağrılır
    protected override void OnContentRendered(EventArgs e)
    {
        if (DataContext is not MainWindowViewModel vm)
        {
            return;
        }

        vm.Start();
    }
}