using Avalonia;
using Avalonia.Controls;

namespace NihonGoo;

public class UIGlobal: AvaloniaObject
{
    
    public static readonly string TransparencyEnabled = nameof(TransparencyEnabled);
    public static readonly AttachedProperty<bool> TransparencyEnabledProperty = AvaloniaProperty.RegisterAttached<UIGlobal, Control, bool>(nameof(TransparencyEnabled), true, true);
    public static void SetTransparencyEnabled(Control obj, bool value) => obj.SetValue(TransparencyEnabledProperty, value);
    public static bool GetTransparencyEnabled(Control obj) => obj.GetValue(TransparencyEnabledProperty);
}