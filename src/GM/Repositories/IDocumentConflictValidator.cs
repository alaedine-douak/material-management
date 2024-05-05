namespace GM.Repositories;

public interface IDocumentConflictValidator
{
    Task<Models.Document?> GetConflictingDocument(Models.Document doc);
}
