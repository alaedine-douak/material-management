namespace GM.Data.Entities;

public class DocumentDetail
{
    public int Id { get; set; }
    public string DocumentNumber { get; set; } = null!;
    public DateTime IssuedDate { get; set; }
    public DateTime EndDate { get; set; }

    public int DocumentId { get; set; }
    public virtual Document Document { get; set; } = null!;
}
