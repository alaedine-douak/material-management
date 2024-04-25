namespace GM.Data.Entities;

public class Document
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;


    public int UserId { get; set; }
    public virtual User User { get; set; } = null!;


    public virtual ICollection<DocumentDetail> DocumentDetails { get; set; } = null!;
}
