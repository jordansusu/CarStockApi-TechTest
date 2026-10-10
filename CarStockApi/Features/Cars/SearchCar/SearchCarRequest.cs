// Define the data required from clients when search cars

namespace CarStockApi.Features.Cars.SearchCar;

public class SearchCarRequest
{
    public string? Brand { get; set; }
    public string? Model { get; set; }
}
