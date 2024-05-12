namespace GM.Models;

public class Document(string name, int? alartDuration)
{
    public string Name { get; } = name;
    public int? AlartDuration { get; } = alartDuration;
}