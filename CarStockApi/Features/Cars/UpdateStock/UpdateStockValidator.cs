// Validator the stock value before updating a car

using FastEndpoints;
using FluentValidation;

namespace CarStockApi.Features.Cars.UpdateStock;

public class UpdateStockValidator : Validator<UpdateStockRequest>
{
    public UpdateStockValidator()
    {
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock amount cannot be a negative number!");
    }
}