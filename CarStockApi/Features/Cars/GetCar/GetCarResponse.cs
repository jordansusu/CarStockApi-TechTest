// Defines the data returned to the client when getting car

namespace CarStockApi.Features.Cars.GetCar;

public class GetCarResponse
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Stock { get; set; }
}
