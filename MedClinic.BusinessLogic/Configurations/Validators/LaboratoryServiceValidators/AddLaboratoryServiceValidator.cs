using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddLaboratoryServiceDto.
/// </summary>
internal class AddLaboratoryServiceValidator : AbstractValidator<AddLaboratoryServiceDto>
{
    public AddLaboratoryServiceValidator()
    {
        // Validate that the Name is not empty
        RuleFor(service => service.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");

        // Validate that the Price is greater than 0
        RuleFor(service => service.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        // Validate that the HospitalServiceId is not an empty GUID
        RuleFor(service => service.HospitalServiceId)
            .NotEmpty()
            .WithMessage("Hospital Service ID must not be empty.");
    }
}