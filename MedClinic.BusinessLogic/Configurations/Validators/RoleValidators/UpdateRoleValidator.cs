using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateRoleDto.
/// </summary>
internal class UpdateRoleValidator : AbstractValidator<UpdateRoleDto>
{
    public UpdateRoleValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(role => role.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the Name is not empty
        RuleFor(role => role.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}