using System.Windows;
using System.Windows.Controls;
using Microsoft.Xaml.Behaviors;

namespace GM.Behaviors;

public class ClearTextTriggerAction : TriggerAction<Button>
{
    public static TextBox GetTarget(DependencyObject obj)
    {
        return (TextBox)obj.GetValue(TargetProperty);
    }

    public static void SetTarget(DependencyObject obj, int value)
    {
        obj.SetValue(TargetProperty, value);
    }

    public static readonly DependencyProperty TargetProperty = DependencyProperty.RegisterAttached(
     "Target", typeof(TextBox), typeof(ClearTextTriggerAction), new PropertyMetadata(null));

    protected override void Invoke(object parameter)
    {
        GetTarget(this)?.Clear();
    }
}
