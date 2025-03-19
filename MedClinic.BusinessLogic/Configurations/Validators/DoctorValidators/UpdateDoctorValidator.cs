using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

public class UpdateDoctorDtoValidator : AbstractValidator<UpdateDoctorDto>
{
    public UpdateDoctorDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Doctor ID is required.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters long.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(50).WithMessage("Last name must be at most 50 characters long.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+998(33|55|77|88|90|91|93|94|95|97|98|99)\d{7}$")
            .WithMessage("Invalid phone number format. It should be in the format: +998 XX XXX XX XX.");

        RuleFor(x => x.BirthDate)
            .NotNull().WithMessage("Birth date is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");

        RuleFor(x => x.PositionId)
            .NotEmpty().WithMessage("Position ID is required.");

        RuleFor(x => x.DoctorRoomId)
            .NotEmpty().WithMessage("Doctor Room ID is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.HospitalServiceId)
            .NotEmpty().WithMessage("Hospital Service ID is required.");

        RuleFor(x => x.BedPercentage)
            .InclusiveBetween(0, 100).WithMessage("Bed percentage must be between 0 and 100.");

        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be greater than zero.");
    }
}