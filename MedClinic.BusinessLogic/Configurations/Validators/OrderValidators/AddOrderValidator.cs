using FluentValidation;
using MedClinic.BusinessLogic.Services;

namespace MedClinic.BusinessLogic.Configurations;

/// <summary>
/// Validator for AddOrderDto.
/// </summary>
internal class AddOrderValidator : AbstractValidator<AddOrderDto>
{
    public AddOrderValidator()
    {
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

        // Validate that the BedNumber is greater than 0
        RuleFor(order => order.BedNumber)
            .GreaterThan(0)
            .WithMessage("Bed number must be greater than 0.");
    }
}