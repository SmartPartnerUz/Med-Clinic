using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

internal class UpdateStatusValidator : AbstractValidator<UpdateStatusDto>
{
    public UpdateStatusValidator()
    {
        RuleFor(status => status.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        RuleFor(status => status.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty.");
    }
}
