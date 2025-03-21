using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdatePayDeskDto.
/// </summary>
internal class UpdatePayDeskValidator : AbstractValidator<UpdatePayDeskDto>
{
    public UpdatePayDeskValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(payDesk => payDesk.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the ReceptionId is not an empty GUID
        RuleFor(payDesk => payDesk.ReceptionId)
            .NotEmpty()
            .WithMessage("Reception ID must not be empty.");

        // Validate that the Income is greater than or equal to 0
        RuleFor(payDesk => payDesk.Income)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Income must be greater than or equal to 0.");

        // Validate that the Expense is greater than or equal to 0
        RuleFor(payDesk => payDesk.Expense)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Expense must be greater than or equal to 0.");
    }
}