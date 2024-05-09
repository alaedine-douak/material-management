using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class User
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR(25)")]
    public string Username { get; set; } = null!;

    public ICollection<Document>? Documents { get; set; }
    public ICollection<Vehicle>? Vehicles { get; set; } 
}
