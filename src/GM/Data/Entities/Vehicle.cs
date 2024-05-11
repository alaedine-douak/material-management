using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string Designation { get; set; }
    public required string Brand { get; set; }
    public required string PlateNumber { get; set; }


    public int UserId { get; set; }
    public User? User { get; set; }


    public ICollection<DocumentInfo>? DocumentInfos { get; set; }
}
