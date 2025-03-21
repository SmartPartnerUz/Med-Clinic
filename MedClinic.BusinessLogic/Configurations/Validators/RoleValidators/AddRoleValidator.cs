using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddRoleDto.
/// </summary>
internal class AddRoleValidator : AbstractValidator<AddRoleDto>
{
    public AddRoleValidator()
    {
        // Validate that the Name is not empty
        RuleFor(role => role.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}