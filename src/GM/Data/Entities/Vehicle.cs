using Microsoft.EntityFrameworkCore;

namespace GM.Data.Entities;

[Index(nameof(Code), nameof(PlateNumber), IsUnique = true)]
public class Vehicle
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Designation { get; set; }
    public string? Brand { get; set; }
    public string? PlateNumber { get; set; }


    public int UserId { get; set; }
    public User? User { get; set; } 
}
