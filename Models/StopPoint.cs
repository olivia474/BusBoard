namespace BusBoard.Models;

public class StopPoint
//declares a public class called StopPoint
{
   public string? NaptanId { get; set; }
   //declares a public property called NaptanId of type string
   //get returns a value for NaptanId
   //set assigns a value for NaptanId
   public string? CommonName { get; set; }
   public float Distance { get; set; }
}