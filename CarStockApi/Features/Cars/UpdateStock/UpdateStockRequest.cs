// Defines the data required from the client when updating car stock

namespace CarStockApi.Features.Cars.UpdateStock;

public class UpdateStockRequest
{
    public int Stock { get; set; }
}