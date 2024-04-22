using GM.ViewModels.Document;

namespace GM.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ViewModelBase CurrentViewModel { get; }

    public MainViewModel()
    {
        CurrentViewModel = new DocumentListViewModel();
    }
}
