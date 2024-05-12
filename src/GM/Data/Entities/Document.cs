namespace GM.Data.Entities;

public class Document
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int AlertDuration { get; set; }


    public int UserId { get; set; }
    public User? User { get; set; }


    public ICollection<DocumentInfo>? DocumentInfos { get; set; }
}
