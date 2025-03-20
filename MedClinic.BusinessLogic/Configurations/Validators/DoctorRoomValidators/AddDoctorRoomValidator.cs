using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddDoctorRoomDto.
/// </summary>
internal class AddDoctorRoomValidator : AbstractValidator<AddDoctorRoomDto>
{
    public AddDoctorRoomValidator()
    {
        // Validate that the Number is greater than 0
        RuleFor(room => room.Number)
            .GreaterThan(0)
            .WithMessage("Room number must be greater than 0.");
    }
}