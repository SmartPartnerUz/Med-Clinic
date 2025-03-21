using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for UpdateOrderDto.
/// </summary>
internal class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
{
    public UpdateOrderValidator()
    {
        // Validate that the Id is not an empty GUID
        RuleFor(order => order.Id)
            .NotEmpty()
            .WithMessage("ID must not be empty.");

        // Validate that the PatientId is not an empty GUID
        RuleFor(order => order.PatientId)
            .NotEmpty()
            .WithMessage("Patient ID must not be empty.");

        // Validate that the RoomId is not an empty GUID
        RuleFor(order => order.RoomId)
            .NotEmpty()
            .WithMessage("Room ID must not be empty.");

        // Validate that the DoctorId is not an empty GUID
        RuleFor(order => order.DoctorId)
            .NotEmpty()
            .WithMessage("Doctor ID must not be empty.");

        // Validate that the HospitalServiceId is not an empty GUID
        RuleFor(order => order.HospitalServiceId)
            .NotEmpty()
            .WithMessage("Hospital Service ID must not be empty.");

        // Validate that the StartDate is not in the past
        RuleFor(order => order.StartDate)
            .GreaterThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Start date must not be in the past.");

        // Validate that the BedNumber is greater than 0
        RuleFor(order => order.BedNumber)
            .GreaterThan(0)
            .WithMessage("Bed number must be greater than 0.");
    }
}