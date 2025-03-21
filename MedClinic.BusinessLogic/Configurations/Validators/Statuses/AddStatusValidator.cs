using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

internal class AddStatusValidator : AbstractValidator<AddStatusDto>
{
    public AddStatusValidator()
    {
        RuleFor(status => status.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}
