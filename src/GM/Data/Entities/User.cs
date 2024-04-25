using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class User
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR(25)")]
    public string Username { get; set; } = null!;

    public virtual ICollection<Document> Document { get; set; } = null!;
}
