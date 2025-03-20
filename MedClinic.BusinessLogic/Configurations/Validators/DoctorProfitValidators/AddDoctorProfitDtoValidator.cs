using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddDoctorProfitDto.
/// </summary>
internal class AddDoctorProfitValidator : AbstractValidator<AddDoctorProfitDto>
{
    public AddDoctorProfitValidator()
    {
        // Validate that the OrderId is not an empty GUID
        RuleFor(profit => profit.OrderId)
            .NotEmpty()
            .WithMessage("Order ID must not be empty.");
    }
}