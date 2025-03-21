using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdatePositionDto.
/// </summary>
internal class UpdatePositionValidator : AbstractValidator<UpdatePositionDto>
{
    public UpdatePositionValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(position => position.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the Name is not empty
        RuleFor(position => position.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}