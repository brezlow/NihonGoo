using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using FluentAvalonia.UI.Windowing;
using NihonGoo.ViewModels;

namespace NihonGoo.Views;

public partial class MainWindow : AppWindow
{
    public MainWindow()
    {
        TitleBar.ExtendsContentIntoTitleBar = true;
        SplashScreen = new MainAppSplashScreen(this);

        InitializeComponent();
    }


    internal class MainAppSplashScreen : IApplicationSplashScreen
    {
        public MainAppSplashScreen(MainWindow owner)
        {
            _owner = owner;
        }

        public string AppName { get; }
        public IImage AppIcon { get; }
        public object SplashScreenContent { get; }
        public int MinimumShowTime => 2000;

        public Action InitApp { get; set; }

        public Task RunTasks(CancellationToken cancellationToken)
        {
            if (InitApp == null)
                return Task.CompletedTask;

            return Task.Run(InitApp, cancellationToken);
        }

        private MainWindow _owner;
    }


    private void OnKanaPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (sender is Border border && border.DataContext is MainWindowViewModel.Kana kana)
        {
            ((MainWindowViewModel)DataContext!).SpeakKana(kana);
        }
    }
}