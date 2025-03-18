using System.Collections.ObjectModel;
using System.Speech.Synthesis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;

namespace NihonGoo.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private bool _darkModeEnabled;
    [ObservableProperty] private bool _borderEnabled;

    [ObservableProperty] private bool _transparencyEnabled;
    [ObservableProperty] private ObservableCollection<WindowTransparencyLevel> _transparencyLevelHint = [];
    [ObservableProperty] private ObservableCollection<Kana> _kanaList;

    private readonly SpeechSynthesizer _synthesizer = new();

    public void SpeakKana(Kana kana)
    {
        if(!kana.IsNotEmpty)return;
        _synthesizer.SelectVoice("Microsoft Haruka Desktop");
        _synthesizer.SpeakAsync(kana.Character+" ");
    }

    public MainWindowViewModel()
    {
        _darkModeEnabled = Application.Current!.ActualThemeVariant == ThemeVariant.Dark;
        _transparencyEnabled = true;
        OnTransparencyEnabledChanged(_transparencyEnabled);
        _kanaList = new ObservableCollection<Kana>
        {
            new("あ", "a"), new("い", "i"), new("う", "u"), new("え", "e"), new("お", "o"),
            new("か", "ka"), new("き", "ki"), new("く", "ku"), new("け", "ke"), new("こ", "ko"),
            new("さ", "sa"), new("し", "shi"), new("す", "su"), new("せ", "se"), new("そ", "so"),
            new("た", "ta"), new("ち", "chi"), new("つ", "tsu"), new("て", "te"), new("と", "to"),
            new("な", "na"), new("に", "ni"), new("ぬ", "nu"), new("ね", "ne"), new("の", "no"),
            new("は", "ha"), new("ひ", "hi"), new("ふ", "fu"), new("へ", "he"), new("ほ", "ho"),
            new("ま", "ma"), new("み", "mi"), new("む", "mu"), new("め", "me"), new("も", "mo"),
            new("や", "ya"), new("", "", false), new("ゆ", "yu"), new("", "", false), new("よ", "yo"),
            new("ら", "ra"), new("り", "ri"), new("る", "ru"), new("れ", "re"), new("ろ", "ro"),
            new("わ", "wa"), new("", "", false), new("", "", false), new("", "", false), new("を", "wo"),
            new("ん", "n"), new("", "", false), new("", "", false), new("", "", false), new("", "", false),
            new("が", "ga"), new("ぎ", "gi"), new("ぐ", "gu"), new("げ", "ge"), new("ご", "go"),
            new("ざ", "za"), new("じ", "ji"), new("ず", "zu"), new("ぜ", "ze"), new("ぞ", "zo"),
            new("だ", "da"), new("ぢ", "ji"), new("づ", "zu"), new("で", "de"), new("ど", "do"),
            new("ば", "ba"), new("び", "bi"), new("ぶ", "bu"), new("べ", "be"), new("ぼ", "bo"),
            new("ぱ", "pa"), new("ぴ", "pi"), new("ぷ", "pu"), new("ぺ", "pe"), new("ぽ", "po"),
            new("きゃ", "kya"), new("きゅ", "kyu"), new("きょ", "kyo"), new("", "", false), new("", "", false),
            new("しゃ", "sha"), new("しゅ", "shu"), new("しょ", "sho"), new("", "", false), new("", "", false),
            new("ちゃ", "cha"), new("ちゅ", "chu"), new("ちょ", "cho"), new("", "", false), new("", "", false),
            new("にゃ", "nya"), new("にゅ", "nyu"), new("にょ", "nyo"), new("", "", false), new("", "", false),
            new("ひゃ", "hya"), new("ひゅ", "hyu"), new("ひょ", "hyo"), new("", "", false), new("", "", false),
            new("みゃ", "mya"), new("みゅ", "myu"), new("みょ", "myo"), new("", "", false), new("", "", false),
            new("りゃ", "rya"), new("りゅ", "ryu"), new("りょ", "ryo"), new("", "", false), new("", "", false),
            new("ぎゃ", "gya"), new("ぎゅ", "gyu"), new("ぎょ", "gyo"), new("", "", false), new("", "", false),
            new("びゃ", "bya"), new("びゅ", "byu"), new("びょ", "byo"), new("", "", false), new("", "", false),
            new("ぴゃ", "pya"), new("ぴゅ", "pyu"), new("ぴょ", "pyo"), new("", "", false), new("", "", false),
        };
    }

    partial void OnDarkModeEnabledChanged(bool value)
    {
        Application.Current!.RequestedThemeVariant = value
            ? ThemeVariant.Dark
            : ThemeVariant.Light;
    }

    partial void OnBorderEnabledChanged(bool value)
    {
        Application.Current!.Resources["UIWindowBorderColorActive"] = value
            ? App.MainWindow.PlatformSettings?.GetColorValues().AccentColor1 ?? Colors.Transparent
            : Colors.Transparent;
    }

    partial void OnTransparencyEnabledChanged(bool value)
    {
        if (value && TransparencyLevelHint.Contains(WindowTransparencyLevel.Mica))
            return;

        TransparencyLevelHint = value
            ? [WindowTransparencyLevel.Mica]
            : [];

        Application.Current!.Resources[nameof(UIGlobal.TransparencyEnabled)] = value;
    }

    public class Kana
    {
        public string Character { get; set; }
        public string Romanization { get; set; }
        public bool IsNotEmpty { get; set; }

        public Kana(string character, string romanization, bool isNotEmpty = true)
        {
            Character = character;
            Romanization = romanization;
            IsNotEmpty = isNotEmpty;
        }
    }
}