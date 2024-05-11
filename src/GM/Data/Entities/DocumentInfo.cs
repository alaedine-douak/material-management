using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class DocumentInfo
{
    public int Id { get; set; }
    public required string DocumentNumber { get; set; }

    [Column(TypeName = "timestamp(0)")]
    public DateTime IssuedDate { get; set; }

    [Column(TypeName = "timestamp(0)")]
    public DateTime EndDate { get; set; }
    public bool IsArchived { get; set; }


    public int DocumentId { get; set; }
    public Document? Document { get; set; }


    public int VehicleId {  get; set; }
    public Vehicle? Vehicle { get; set; }
}
