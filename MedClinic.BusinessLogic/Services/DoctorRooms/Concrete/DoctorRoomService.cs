using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.DoctorRooms;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing doctor rooms.
/// </summary>
public class DoctorRoomService : IDoctorRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DoctorRoomService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddDoctorRoomDto> _addDoctorRoomValidator;
    private readonly IValidator<UpdateDoctorRoomDto> _updateDoctorRoomValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="DoctorRoomService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addDoctorRoomValidator">The validator for adding a doctor room.</param>
    /// <param name="updateDoctorRoomValidator">The validator for updating a doctor room.</param>
    public DoctorRoomService(
        IUnitOfWork unitOfWork,
        ILogger<DoctorRoomService> logger,
        IMapper mapper,
        IValidator<AddDoctorRoomDto> addDoctorRoomValidator,
        IValidator<UpdateDoctorRoomDto> updateDoctorRoomValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addDoctorRoomValidator = addDoctorRoomValidator;
        _updateDoctorRoomValidator = updateDoctorRoomValidator;
    }

    /// <summary>
    /// Creates a new doctor room.
    /// </summary>
    /// <param name="doctorRoom">The doctor room DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created doctor room.</returns>
    public async Task<(bool, Guid)> CreateDoctorRoom(AddDoctorRoomDto doctorRoom)
    {
        try
        {
            // Validate the doctor room DTO
            var validationResult = _addDoctorRoomValidator.Validate(doctorRoom);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to DoctorRoom entity
            var newDoctorRoom = _mapper.Map<DoctorRoom>(doctorRoom);
            newDoctorRoom.Id = Guid.NewGuid();
            newDoctorRoom.CreatedAt = DateTime.UtcNow;

            // Add doctor room to the repository
            var isCreated = await _unitOfWork.DoctorRoomWrite.AddAsync(newDoctorRoom);

            if (!isCreated)
            {
                _logger.LogError("Doctor room creation failed for {Id}.", newDoctorRoom.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Doctor room {DoctorRoomId} created successfully.", newDoctorRoom.Id);
            return (true, newDoctorRoom.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Doctor room creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the doctor room.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing doctor room.
    /// </summary>
    /// <param name="doctorRoom">The doctor room DTO.</param>
    public async Task UpdateDoctorRoom(UpdateDoctorRoomDto doctorRoom)
    {
        try
        {
            var validationResult = _updateDoctorRoomValidator.Validate(doctorRoom);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingDoctorRoom = await _unitOfWork.DoctorRoomRead.GetByIdAsync(doctorRoom.Id);
            if (existingDoctorRoom == null)
            {
                _logger.LogWarning("Doctor room with ID {DoctorRoomId} not found.", doctorRoom.Id);
                throw new KeyNotFoundException($"Doctor room with ID {doctorRoom.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(doctorRoom, existingDoctorRoom);

            var isUpdated = _unitOfWork.DoctorRoomWrite.Update(existingDoctorRoom);

            if (!isUpdated)
            {
                _logger.LogError("Doctor room update failed for {DoctorRoomId}.", doctorRoom.Id);
                throw new Exception($"Doctor room update failed for {doctorRoom.Id}");
            }

            _logger.LogInformation("Doctor room {DoctorRoomId} updated successfully.", doctorRoom.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating doctor room {DoctorRoomId}.", doctorRoom.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a doctor room by ID.
    /// </summary>
    /// <param name="id">The doctor room ID.</param>
    public async Task DeleteDoctorRoom(Guid id)
    {
        try
        {
            var doctorRoom = await _unitOfWork.DoctorRoomRead.GetByIdAsync(id);
            if (doctorRoom == null)
            {
                _logger.LogWarning("Doctor room with ID {DoctorRoomId} not found.", id);
                throw new KeyNotFoundException($"Doctor room with ID {id} not found.");
            }

            _unitOfWork.DoctorRoomWrite.Delete(doctorRoom);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Doctor room {DoctorRoomId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting doctor room with ID {DoctorRoomId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all doctor rooms with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of doctor room DTOs.</returns>
    public PagedResult<DoctorRoomDto> GetAllDoctorRooms(DoctorRoomSortFilterOptions options)
    {
        var doctorRooms = _unitOfWork.DoctorRoomRead.GetAll().AsNoTracking();
        var doctorRoomDtos = doctorRooms.Select(dr => _mapper.Map<DoctorRoomDto>(dr)).AsPagedResult(options);
        return doctorRoomDtos;
    }

    /// <summary>
    /// Gets a doctor room by ID.
    /// </summary>
    /// <param name="id">The doctor room ID.</param>
    /// <returns>The doctor room DTO.</returns>
    public async Task<DoctorRoomDto> GetDoctorRoomById(Guid id)
    {
        var doctorRoom = await _unitOfWork.DoctorRoomRead.GetByIdAsync(id);
        if (doctorRoom == null)
        {
            _logger.LogWarning("Doctor room with ID {DoctorRoomId} not found.", id);
            throw new KeyNotFoundException($"Doctor room with ID {id} not found.");
        }

        return _mapper.Map<DoctorRoomDto>(doctorRoom);
    }
}