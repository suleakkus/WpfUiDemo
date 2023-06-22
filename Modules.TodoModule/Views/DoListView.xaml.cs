using System.Windows.Controls;
using System.Windows.Input;
using Modules.TodoModule.ViewModels;
using Prism.Events;

namespace Modules.TodoModule.Views;

public partial class DoListView
{
    public DoListView(IEventAggregator ea)
    {
        InitializeComponent();
        ea.GetEvent<Common.Events.LoginEvent>().Subscribe(delegate { FixScroll(); });
    }

    private void FixScroll()
    {
        ScrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
    }

    private void TextBoxBaseOnTextChanged(object sender, KeyEventArgs e)
    {
        if (e.Key != Key.Enter)
        {
            return;
        }

        var textBox = (TextBox)sender;
        var viewModel = (DoListViewModel)DataContext;
        viewModel.CreateSection(textBox.Text);
        textBox.Text = string.Empty;
    }
}