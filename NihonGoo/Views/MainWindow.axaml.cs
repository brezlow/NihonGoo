using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
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

    private static Stream GetResource(string loc)
    {
        return AssetLoader.Open(new Uri(loc));
    }

    internal class MainAppSplashScreen : IApplicationSplashScreen
    {
        public MainAppSplashScreen(MainWindow owner)
        {
            _owner = owner;
        }

        public string AppName { get; init; }
        public IImage AppIcon { get; init; }

     
        public object SplashScreenContent => new Image
        {
            Source = new Bitmap(GetResource("avares://NihonGoo/Assets/logo.png"))
        };

        public int MinimumShowTime => 1000;

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