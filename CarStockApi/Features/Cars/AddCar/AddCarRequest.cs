// Define the data required from the client when adding a new car

namespace CarStockApi.Features.Cars.AddCar;

public class AddCarRequest
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Stock {get; set; }
}