using Prism.Mvvm;

namespace WpfUiDemo.Desktop.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string title;

    public MainWindowViewModel()
    {
        title = "Awesome Title";
    }
    
    public string Title
    {
        get => title;
        set => SetProperty(ref title, value);
    }
}