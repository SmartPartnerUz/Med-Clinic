using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddPatientDto.
/// </summary>
internal class AddPatientValidator : AbstractValidator<AddPatientDto>
{
    public AddPatientValidator()
    {
        // Validate that the UserId is not an empty GUID
        RuleFor(patient => patient.UserId)
            .NotEmpty()
            .WithMessage("User ID must not be empty.");
    }
}