namespace BusBoard.Models;

public class PostcodeData
//declaring a public class PostcodeData
{
    public string? Postcode { get; set; }
    //declares a public property called Postcode of type string
    //get returns a value for Postcode
    //set assigns a value for Postcode
    public float Latitude { get; set; }
    public float Longitude { get; set; }

}