// Defines the data returned to the client when searching for cars.

namespace CarStockApi.Features.Cars.SearchCar;

public class SearchCarResponse
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set;} = string.Empty;
    public int Year { get; set; }
    public int Stock { get; set; }
}