using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateHospitalServiceDto.
/// </summary>
internal class UpdateHospitalServiceValidator : AbstractValidator<UpdateHospitalServiceDto>
{
    public UpdateHospitalServiceValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(service => service.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

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