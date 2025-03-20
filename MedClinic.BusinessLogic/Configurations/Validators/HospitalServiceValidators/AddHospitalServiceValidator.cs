using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddHospitalServiceDto.
/// </summary>
internal class AddHospitalServiceValidator : AbstractValidator<AddHospitalServiceDto>
{
    public AddHospitalServiceValidator()
    {
        // Validate that the Name is not empty
        RuleFor(service => service.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");

        // Validate that the Price is greater than 0
        RuleFor(service => service.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");
    }
}