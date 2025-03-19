using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations.Validators;

public class UpdateDtoValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters long.");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Birth date is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");
    }
}