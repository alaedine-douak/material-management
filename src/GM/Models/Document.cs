namespace GM.Models;

public class Document(string name, int? alartedDuration)
{
    public string Name { get; } = name;
    public int? AlartedDuration { get; } = alartedDuration;
}