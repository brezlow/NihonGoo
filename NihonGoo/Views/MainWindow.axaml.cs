using Avalonia.Controls;
using Avalonia.Input;
using NihonGoo.ViewModels;

namespace NihonGoo.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnKanaPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is MainWindowViewModel.Kana kana)
        {
            ((MainWindowViewModel)DataContext!).SpeakKana(kana);
        }
    }
}