namespace BusBoard.Models;

public class ArrivalPrediction
//declaring a public class called ArrivalPrediction
{
    public int TimeToStation { get; set; }
    //declares a public property called TimeToStation of type int
     //get returns a value for TimeToStation
     //set assigns value to TimeToStation 
    public string? LineName { get; set; }
    public string? DestinationName { get; set; }

}