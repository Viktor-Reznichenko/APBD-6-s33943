using APBD_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_6.Controllers;

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
    public IActionResult Post(Room room)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        room.Id = rooms.Max(r => r.Id) + 1;
        rooms.Add(room);

        return CreatedAtAction(nameof(GetById), new { id = room.Id }, room);
    }

    
}