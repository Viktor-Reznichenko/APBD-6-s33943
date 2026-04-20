namespace APBD_6.DTOs;

public class CreateReservationDTO
{
    public int RoomId{get;set;}
    public string OrganizerName{get;set;}
    public string Topic{get;set;}
    public DateTime Date{get;set;}
    public DateTime StartTime{get;set;}
    public DateTime EndTime{get;set;}
    public string Status{get;set;}
}