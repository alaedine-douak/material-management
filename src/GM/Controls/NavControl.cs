using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GM.Controls;

public class NavControl : ListBoxItem
{
    static NavControl()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(NavControl), 
            new FrameworkPropertyMetadata(typeof(NavControl)));
    }

    public string Header
    {
        get { return (string)GetValue(HeaderProperty); }
        set { SetValue(HeaderProperty, value); }
    }

    public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(
        "Header", 
        typeof(string), 
        typeof(NavControl), 
        new PropertyMetadata(null));


    public Geometry NavIcon
    {
        get { return (Geometry)GetValue(NavIconProperty); }
        set { SetValue(NavIconProperty, value); }
    }

    public static readonly DependencyProperty NavIconProperty = DependencyProperty.Register(
        "NavIcon", 
        typeof(Geometry), 
        typeof(NavControl),
        new PropertyMetadata(null));




    //public Geometry Icon
    //{
    //    get { return (Geometry)GetValue(IconProperty); }
    //    set { SetValue(IconProperty, value); }
    //}

    //public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
    //    "Icon", 
    //    typeof(Geometry), 
    //    typeof(NavControl), 
    //    new PropertyMetadata(null));


}
