namespace BusBoard.Models;

public class PostcodeResponse
//decalres a public class PostcodeResponse
{
    public int Status { get; set; }
    //declares a public property called Status of type int
    //get returns a value for Status
    //set assigns a value for Status
    public PostcodeData? Result { get; set; }
}