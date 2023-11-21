using Newtonsoft.Json;
using BusBoard.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.Marshalling;

namespace BusBoard;

internal class Program
//internal = can only be accessed by code in the same class
//declaring a class called Program
{
    async static Task Busboard()
    {
        string postcode = "BR1 3TX";
        //declaring a string variable called postcode and assigning it to a value

        var tflClient = new HttpClient();
        //declaring a variable called tflClient
        tflClient.BaseAddress = new Uri("https://api.tfl.gov.uk/StopPoint/");

        var postcodeClient = new HttpClient();
        //declaring a varuable called postcodeClient..
        postcodeClient.BaseAddress = new Uri("https://api.postcodes.io/postcodes/");

        var postcodeJson = await postcodeClient.GetStringAsync(postcode);
        //declaring a variable called postcodeJson
        var postcodeResponse = JsonConvert.DeserializeObject<PostcodeResponse>(postcodeJson);
        //declaring a variable called postcodeResponse...
        var postcodeData = postcodeResponse!.Result;
        //declaring a variable called postcodeData and assigning it the value of the result property
        var lat = postcodeData!.Latitude;
        //declaring a variable called lat and assigning it the value of the latitude property
        var lon = postcodeData.Longitude;
        //declaring a variable called lon and assigning it the value of the longitude property

        // find closest stops
        var stopsJson = await tflClient.GetStringAsync($"?lat={lat}&lon={lon}&stopTypes=NaptanPublicBusCoachTram&radius=500");
        var stopsResponse = JsonConvert.DeserializeObject<StopPointsResponse>(stopsJson);
        var closestStop = stopsResponse!.StopPoints!.OrderBy(s => s.Distance).First();

        Console.WriteLine($"Closest stop is \"{closestStop.CommonName}\" ({closestStop.Distance}m)");

        // find next 5 buses
        var arrivalsJson = await tflClient.GetStringAsync($"{closestStop.NaptanId}/Arrivals");  
        var predictions = JsonConvert.DeserializeObject<List<ArrivalPrediction>>(arrivalsJson);
        
        if (predictions is null)
        {
            throw new Exception("Unable to retrieve predictions from JSON response");
        }

        var firstFiveBuses = predictions.OrderBy(p => p.TimeToStation).Take(5);

        foreach (var p in predictions)
        {
            Console.WriteLine($"{p.LineName} bus to {p.DestinationName} arriving in {p.TimeToStation} seconds");
        }
    }
    async static Task Main(string[] args)
    {
        await Busboard();
        //calling a method
    }
}