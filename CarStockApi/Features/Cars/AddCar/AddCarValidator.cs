// Defines validation rules for AddCarRequest

using FastEndpoints;
using FluentValidation;

namespace CarStockApi.Features.Cars.AddCar;
public class AddCarValidator : Validator<AddCarRequest>
{
    public AddCarValidator()
    {
        int currentYear = DateTime.Today.Year;

        RuleFor(x => x.Brand)
            .NotEmpty()
            .WithMessage("Brand name is required!")
            .Matches(@"^[\p{L}\p{N} .&\-]+$")
            .WithMessage("Brand name can only contain letters, numbers, spaces, hyphens(-), dots(.), and ampersands(&)!");

        RuleFor(x => x.Model)
            .NotEmpty()
            .WithMessage("Model name is required!")
            .Matches(@"^[\p{L}\p{N} .\/+\-]+$")
            .WithMessage("Model name can only contain letters, numbers, spaces, and characters like ( . / + - )!");

        RuleFor(x => x.Year)
            .InclusiveBetween(1886, currentYear) //Check again!!!
            .WithMessage($"Year must be a valid year between 1 and {currentYear}!");
        
        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock amount cannot be a negative number!");
    }
}