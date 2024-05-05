using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class DocumentInfo
{
    public int Id { get; set; }
    public string DocumentNumber { get; set; } = null!;
    
    [Column(TypeName = "timestamp(0)")]
    public DateTime IssuedDate { get; set; }

    [Column(TypeName = "timestamp(0)")]
    public DateTime EndDate { get; set; }

    public int DocumentId { get; set; }
    public virtual Document Document { get; set; } = null!;
}
