using System.Windows.Controls;
using System.Windows.Input;

namespace GM.Controls;

public class SearchComboBox : ComboBox
{
    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (IsEditable &&
            IsDropDownOpen == false &&
            StaysOpenOnEdit)
        {
            IsDropDownOpen = true;
        }
    }
}
