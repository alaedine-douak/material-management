namespace GM.Models;

public class Document(string name)
{
    public string Name { get; } = name;

    public bool ConflictDocumentName(Document document)
    {
        if (document.Name.ToLower() !=  Name.ToLower()) return false;

        return true;
    }
}