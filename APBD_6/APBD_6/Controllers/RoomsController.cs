using APBD_6.DTOs;
using APBD_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_6.Controllers;

[Route("api/[controller]")]
[ApiController] 
public class RoomsController : ControllerBase
{
    public static List<Room> rooms = new List<Room>()
    {
     new Room(){Id =1, Name ="254", BuildingCode = "B", Capacity  = 16, Floor = 3, HasProjector = true, IsActive = true},  
    };
    //GET api/rooms
    [HttpGet]
    public IActionResult Get([FromQuery] int? id = 0)
    {
        // 200 ok
        return Ok(rooms);
    }
    
    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById(int id)
    {
        var room = rooms.FirstOrDefault(x => x.Id == id);
        if (room == null)
        {
            return NotFound();
        }
        return Ok(room);
    }
    
    [Route("building/{buildingCode}")]
    [HttpGet]
    public IActionResult GetByBuilding(string buildingCode)
    {
        var buildingRooms = rooms.Where(x => x.BuildingCode.Equals(buildingCode)).ToList();
        if (buildingRooms == null)
        {
            return NotFound();
        }
        return Ok(buildingRooms);
    }
    
    
    [HttpPost]
    public IActionResult Post([FromBody] CreateRoomDTO roomDTO)
    {
        var room = new Room()
        {
            Id = rooms.Count + 1,
            Name = roomDTO.Name,
            BuildingCode = roomDTO.BuildingCode,
            Capacity = roomDTO.Capacity,
            IsActive = roomDTO.IsActive,
            HasProjector = roomDTO.HasProjector,
            Floor = roomDTO.Floor,
            
        };
        rooms.Add(room);
        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult Update(int id, Room room)
    {

        var existing = rooms.FirstOrDefault(r => r.Id == id);
        if (existing == null)
            return NotFound();

        existing.Name = room.Name;
        existing.BuildingCode = room.BuildingCode;
        existing.Floor = room.Floor;
        existing.Capacity = room.Capacity;
        existing.HasProjector = room.HasProjector;
        existing.IsActive = room.IsActive;

        return Ok(existing);
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var room = rooms.FirstOrDefault(r => r.Id == id);
        if (room == null)
            return NotFound();

        rooms.Remove(room);
        return NoContent();
    }
}

