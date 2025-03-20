using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddBedDto.
/// </summary>
internal class AddBedValidator : AbstractValidator<AddBedDto>
{
    public AddBedValidator()
    {
        // Validate that the bed number is greater than 0
        RuleFor(bed => bed.Number)
            .GreaterThan(0)
            .WithMessage("Bed number must be greater than 0.");

        // Validate that the RoomId is not an empty GUID
        RuleFor(bed => bed.RoomId)
            .NotEmpty()
            .WithMessage("Room ID must not be empty.");
    }
}
