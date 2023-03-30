using System.Windows.Controls;
using System.Windows.Input;
using Modules.TodoModule.ViewModels;

namespace Modules.TodoModule.Views;

public partial class DoListView
{
    public DoListView()
    {
        InitializeComponent();
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