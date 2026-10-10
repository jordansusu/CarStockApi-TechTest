// Defines the data returned to the client when listing cars

namespace CarStockApi.Features.Cars.ListCars;
public class ListCarsResponse
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Stock { get; set; }
}