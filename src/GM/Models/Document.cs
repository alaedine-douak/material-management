namespace GM.Models;

public class Document
{
    public string Name { get; }

    public Document(string name) => Name = name;

    public bool ConflictDocumentName(Document document)
    {
        if (document.Name.ToLower() !=  Name.ToLower()) return false;

        return true;
    }
}