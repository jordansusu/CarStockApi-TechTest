// Defines the validation rules for SearchCarRequest

using FastEndpoints;
using FluentValidation;

namespace CarStockApi.Features.Cars.SearchCar;

public class SearchCarRequestValidator : Validator<SearchCarRequest>
{
    public SearchCarRequestValidator()
    {
        RuleFor(x => x.Brand)
            .Matches(@"^[\p{L}\p{N} .&\-]+$")
            .WithMessage("Brand name can only contain letters, numbers, spaces, hyphens (-), dots (.), and ampersands (&)!")
            .When(x => !string.IsNullOrWhiteSpace(x.Brand));
        
        RuleFor(x => x.Model)
            .Matches(@"^[\p{L}\p{N} .\/+\-]+$")
            .WithMessage("Model name can only contain letters, numbers, spaces, and characters like ( . / + - )!")
            .When(x => !string.IsNullOrWhiteSpace(x.Model));

        RuleFor(x => x)
            .Must(x =>
                !string.IsNullOrWhiteSpace(x.Brand) ||
                !string.IsNullOrWhiteSpace(x.Model))
            .WithMessage("Please provide a Brand or Model to search!");
    }
}