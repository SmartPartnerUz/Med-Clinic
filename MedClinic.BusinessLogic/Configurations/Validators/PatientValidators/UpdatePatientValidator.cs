using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdatePatientDto.
/// </summary>
internal class UpdatePatientValidator : AbstractValidator<UpdatePatientDto>
{
    public UpdatePatientValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(patient => patient.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the UserId is not an empty GUID
        RuleFor(patient => patient.UserId)
            .NotEmpty()
            .WithMessage("User ID must not be empty.");
    }
}