using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateFirstViewOrderDto.
/// </summary>
internal class UpdateFirstViewOrderValidator : AbstractValidator<UpdateFirstViewOrderDto>
{
    public UpdateFirstViewOrderValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(order => order.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the Queue is greater than 0
        RuleFor(order => order.Queue)
            .GreaterThan(0)
            .WithMessage("Queue must be greater than 0.");

        // Validate that the DoctorId is not an empty GUID
        RuleFor(order => order.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor ID must not be empty.");

        // Validate that the PatientId is not an empty GUID
        RuleFor(order => order.PatientId)
            .NotEmpty()
            .WithMessage("Patient ID must not be empty.");
    }
}