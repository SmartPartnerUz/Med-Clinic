using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateDoctorProfitDto.
/// </summary>
internal class UpdateDoctorProfitValidator : AbstractValidator<UpdateDoctorProfitDto>
{
    public UpdateDoctorProfitValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(profit => profit.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the OrderId is not an empty GUID
        RuleFor(profit => profit.OrderId)
            .NotEmpty()
            .WithMessage("Order ID must not be empty.");
    }
}
