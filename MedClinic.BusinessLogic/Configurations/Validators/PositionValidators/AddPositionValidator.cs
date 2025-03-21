using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddPositionDto.
/// </summary>
internal class AddPositionValidator : AbstractValidator<AddPositionDto>
{
    public AddPositionValidator()
    {
        // Validate that the Name is not empty
        RuleFor(position => position.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}