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
        if (!kana.IsNotEmpty) return;
        _synthesizer.SelectVoice("Microsoft Haruka Desktop");
        _synthesizer.SpeakAsync(kana.Character + " ");
    }

    public MainWindowViewModel()
    {
        _darkModeEnabled = Application.Current!.ActualThemeVariant == ThemeVariant.Dark;
        _transparencyEnabled = true;
        OnTransparencyEnabledChanged(_transparencyEnabled);
        _kanaList = new ObservableCollection<Kana>
        {
            new("あ/ア", "a"), new("い/イ", "i"), new("う/ウ", "u"), new("え/エ", "e"), new("お/オ", "o"),
            new("か/カ", "ka"), new("き/キ", "ki"), new("く/ク", "ku"), new("け/ケ", "ke"), new("こ/コ", "ko"),
            new("さ/サ", "sa"), new("し/シ", "shi"), new("す/ス", "su"), new("せ/セ", "se"), new("そ/ソ", "so"),
            new("た/タ", "ta"), new("ち/チ", "chi"), new("つ/ツ", "tsu"), new("て/テ", "te"), new("と/ト", "to"),
            new("な/ナ", "na"), new("に/ニ", "ni"), new("ぬ/ヌ", "nu"), new("ね/ネ", "ne"), new("の/ノ", "no"),
            new("は/ハ", "ha"), new("ひ/ヒ", "hi"), new("ふ/フ", "fu"), new("へ/ヘ", "he"), new("ほ/ホ", "ho"),
            new("ま/マ", "ma"), new("み/ミ", "mi"), new("む/ム", "mu"), new("め/メ", "me"), new("も/モ", "mo"),
            new("や/ヤ", "ya"), new("", "", false), new("ゆ/ユ", "yu"), new("", "", false), new("よ/ヨ", "yo"),
            new("ら/ラ", "ra"), new("り/リ", "ri"), new("る/ル", "ru"), new("れ/レ", "re"), new("ろ/ロ", "ro"),
            new("わ/ワ", "wa"), new("", "", false), new("", "", false), new("", "", false), new("を/ヲ", "wo"),
            new("ん/ン", "n"), new("", "", false), new("", "", false), new("", "", false), new("", "", false),
            new("が/ガ", "ga"), new("ぎ/ギ", "gi"), new("ぐ/グ", "gu"), new("げ/ゲ", "ge"), new("ご/ゴ", "go"),
            new("ざ/ザ", "za"), new("じ/ジ", "ji"), new("ず/ズ", "zu"), new("ぜ/ゼ", "ze"), new("ぞ/ゾ", "zo"),
            new("だ/ダ", "da"), new("ぢ/ヂ", "ji"), new("づ/ヅ", "zu"), new("で/デ", "de"), new("ど/ド", "do"),
            new("ば/バ", "ba"), new("び/ビ", "bi"), new("ぶ/ブ", "bu"), new("べ/ベ", "be"), new("ぼ/ボ", "bo"),
            new("ぱ/パ", "pa"), new("ぴ/ピ", "pi"), new("ぷ/プ", "pu"), new("ぺ/ペ", "pe"), new("ぽ/ポ", "po"),
            new("きゃ/キャ", "kya"), new("きゅ/キュ", "kyu"), new("きょ/キョ", "kyo"), new("", "", false), new("", "", false),
            new("しゃ/シャ", "sha"), new("しゅ/シュ", "shu"), new("しょ/ショ", "sho"), new("", "", false), new("", "", false),
            new("ちゃ/チャ", "cha"), new("ちゅ/チュ", "chu"), new("ちょ/チョ", "cho"), new("", "", false), new("", "", false),
            new("にゃ/ニャ", "nya"), new("にゅ/ニュ", "nyu"), new("にょ/ニョ", "nyo"), new("", "", false), new("", "", false),
            new("ひゃ/ヒャ", "hya"), new("ひゅ/ヒュ", "hyu"), new("ひょ/ヒョ", "hyo"), new("", "", false), new("", "", false),
            new("みゃ/ミャ", "mya"), new("みゅ/ミュ", "myu"), new("みょ/ミョ", "myo"), new("", "", false), new("", "", false),
            new("りゃ/リャ", "rya"), new("りゅ/リュ", "ryu"), new("りょ/リョ", "ryo"), new("", "", false), new("", "", false),
            new("ぎゃ/ギャ", "gya"), new("ぎゅ/ギュ", "gyu"), new("ぎょ/ギョ", "gyo"), new("", "", false), new("", "", false),
            new("びゃ/ビャ", "bya"), new("びゅ/ビュ", "byu"), new("びょ/ビョ", "byo"), new("", "", false), new("", "", false),
            new("ぴゃ/ピャ", "pya"), new("ぴゅ/ピュ", "pyu"), new("ぴょ/ピョ", "pyo"), new("", "", false)
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