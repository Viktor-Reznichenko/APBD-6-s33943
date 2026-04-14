namespace APBD_6.Models;

public class Room
{
    public int Id{get;set;}
    public string Name{get;set;}
    public char BuildingCode{get;set;}
    public int Floor{get;set;}
    public int Capacity{get;set;}
    public bool HasProjector { get; set; }
    public bool IsActive { get; set; }
}