using GM.ViewModels;

namespace GM.Models;

public class Document(
    string name, 
    int? alartDuration) : ViewModelBase
{
    public string Name { get; } = name;
    public int? AlartDuration { get; } = alartDuration;
}