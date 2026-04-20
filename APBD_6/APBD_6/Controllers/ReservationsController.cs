using APBD_6.DTOs;
using APBD_6.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_6.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    public static List<Reservation> reservations = new List<Reservation>()
    {
        new Reservation(){Id =1, RoomId = 1, StartTime = new DateTime(2026, 5, 10, 8, 30, 0), EndTime = new DateTime(2026, 4, 21, 10, 0, 0), Date = new DateTime(2026, 4, 21), OrganizerName = "John Doe", Status = "Active", Topic = "PJC"},  
    };
    
    [HttpGet]
    public IActionResult Get([FromQuery] int? id = 0)
    {
        // 200 ok
        return Ok(reservations);
    }
    
    [Route("{id}")]
    [HttpGet]
    public IActionResult GetById(int id)
    {
        var reservation = reservations.FirstOrDefault(x => x.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }
    
    
    
    
    [HttpPost]
    public IActionResult Post([FromBody] CreateReservationDTO reservationDTO)
    {
        var reservation = new Reservation()
        {
            Id = reservations.Count + 1,
            RoomId = reservationDTO.RoomId,
            OrganizerName = reservationDTO.OrganizerName,
            StartTime = reservationDTO.StartTime,
            EndTime = reservationDTO.EndTime,
            Status = reservationDTO.Status,
            Topic = reservationDTO.Topic,
            Date = reservationDTO.Date
        };
        if (reservation.EndTime <= reservation.StartTime)
        {
            return BadRequest();
        }

        var room = RoomsController.rooms.FirstOrDefault(r => r.Id == reservation.RoomId);
        if (room == null)
            return NotFound("Room does not exist");

        if (!room.IsActive)
            return Conflict("Room is inactive");

        bool overlap = reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date.Date == reservation.Date.Date &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (overlap)
            return Conflict("Reservation overlaps with existing one");

        reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Reservation reservation)
    {
        if (reservation.EndTime <= reservation.StartTime)
            return BadRequest();

        var existing = reservations.FirstOrDefault(r => r.Id == id);
        if (existing == null)
            return NotFound();

        existing.RoomId = reservation.RoomId;
        existing.OrganizerName = reservation.OrganizerName;
        existing.Topic = reservation.Topic;
        existing.Date = reservation.Date;
        existing.StartTime = reservation.StartTime;
        existing.EndTime = reservation.EndTime;
        existing.Status = reservation.Status;

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var reservation = reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
            return NotFound();

        reservations.Remove(reservation);
        return NoContent();
    }

}