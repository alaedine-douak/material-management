using System.ComponentModel.DataAnnotations.Schema;

namespace GM.Data.Entities;

public class Vehicle
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Designation { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string PlateNumber { get; set; } = null!;


    public int UserId { get; set; }
    public User? User { get; set; } 
}
