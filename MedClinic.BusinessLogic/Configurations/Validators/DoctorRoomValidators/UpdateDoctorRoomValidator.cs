using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateDoctorRoomDto.
/// </summary>
internal class UpdateDoctorRoomValidator : AbstractValidator<UpdateDoctorRoomDto>
{
    public UpdateDoctorRoomValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(room => room.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the Number is greater than 0
        RuleFor(room => room.Number)
            .GreaterThan(0)
            .WithMessage("Room number must be greater than 0.");
    }
}