﻿using Microsoft.EntityFrameworkCore;

namespace GM.Data.Entities;

[Index(nameof(Code), nameof(PlateNumber), IsUnique = true)]
public class Vehicle
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Designation { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string PlateNumber { get; set; } = null!;


    public int UserId { get; set; }
    public User? User { get; set; } 
}
