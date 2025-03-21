using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.Rooms;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing rooms.
/// </summary>
public class RoomService : IRoomService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<RoomService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddRoomDto> _addRoomValidator;
    private readonly IValidator<UpdateRoomDto> _updateRoomValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="RoomService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addRoomValidator">The validator for adding a room.</param>
    /// <param name="updateRoomValidator">The validator for updating a room.</param>
    public RoomService(
        IUnitOfWork unitOfWork,
        ILogger<RoomService> logger,
        IMapper mapper,
        IValidator<AddRoomDto> addRoomValidator,
        IValidator<UpdateRoomDto> updateRoomValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addRoomValidator = addRoomValidator;
        _updateRoomValidator = updateRoomValidator;
    }

    /// <summary>
    /// Creates a new room.
    /// </summary>
    /// <param name="room">The room DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created room.</returns>
    public async Task<(bool, Guid)> CreateRoom(AddRoomDto room)
    {
        try
        {
            // Validate the room DTO
            var validationResult = _addRoomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to Room entity
            var newRoom = _mapper.Map<Room>(room);
            newRoom.Id = Guid.NewGuid();
            newRoom.CreatedAt = DateTime.UtcNow;

            // Add room to the repository
            var isCreated = await _unitOfWork.RoomWrite.AddAsync(newRoom);

            if (!isCreated)
            {
                _logger.LogError("Room creation failed for {Id}.", newRoom.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Room {RoomId} created successfully.", newRoom.Id);
            return (true, newRoom.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Room creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the room.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing room.
    /// </summary>
    /// <param name="room">The room DTO.</param>
    public async Task UpdateRoom(UpdateRoomDto room)
    {
        try
        {
            var validationResult = _updateRoomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingRoom = await _unitOfWork.RoomRead.GetByIdAsync(room.Id);
            if (existingRoom == null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", room.Id);
                throw new KeyNotFoundException($"Room with ID {room.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(room, existingRoom);

            var isUpdated = _unitOfWork.RoomWrite.Update(existingRoom);

            if (!isUpdated)
            {
                _logger.LogError("Room update failed for {RoomId}.", room.Id);
                throw new Exception($"Room update failed for {room.Id}");
            }

            _logger.LogInformation("Room {RoomId} updated successfully.", room.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating room {RoomId}.", room.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a room by ID.
    /// </summary>
    /// <param name="id">The room ID.</param>
    public async Task DeleteRoom(Guid id)
    {
        try
        {
            var room = await _unitOfWork.RoomRead.GetByIdAsync(id);
            if (room == null)
            {
                _logger.LogWarning("Room with ID {RoomId} not found.", id);
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }

            _unitOfWork.RoomWrite.Delete(room);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Room {RoomId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting room with ID {RoomId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all rooms with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of room DTOs.</returns>
    public PagedResult<RoomDto> GetAllRooms(RoomSortFilterOptions options)
    {
        var rooms = _unitOfWork.RoomRead.GetAll().AsNoTracking();
        var roomDtos = rooms.Select(r => _mapper.Map<RoomDto>(r)).AsPagedResult(options);
        return roomDtos;
    }

    /// <summary>
    /// Gets a room by ID.
    /// </summary>
    /// <param name="id">The room ID.</param>
    /// <returns>The room DTO.</returns>
    public async Task<RoomDto> GetRoomById(Guid id)
    {
        var room = await _unitOfWork.RoomRead.GetByIdAsync(id);
        if (room == null)
        {
            _logger.LogWarning("Room with ID {RoomId} not found.", id);
            throw new KeyNotFoundException($"Room with ID {id} not found.");
        }

        return _mapper.Map<RoomDto>(room);
    }
}