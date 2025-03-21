using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddRoomDto.
/// </summary>
internal class AddRoomValidator : AbstractValidator<AddRoomDto>
{
    public AddRoomValidator()
    {
        // Validate that the Number is greater than 0
        RuleFor(room => room.Number)
            .GreaterThan(0)
            .WithMessage("Room number must be greater than 0.");

        // Validate that the StatusId is not an empty GUID
        RuleFor(room => room.StatusId)
            .NotEmpty()
            .WithMessage("Status ID must not be empty.");

        // Validate that the Price is greater than 0
        RuleFor(room => room.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}