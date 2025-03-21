using AutoMapper;
using FluentValidation;
using MedClinic.BusinessLogic.Extentions;
using MedClinic.DataAccess.Interfaces;
using MedClinic.Domain.Entities.PayDesks;
using MedClinic.Domain.Models.PageResult;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Service for managing pay desks.
/// </summary>
public class PayDeskService : IPayDeskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PayDeskService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<AddPayDeskDto> _addPayDeskValidator;
    private readonly IValidator<UpdatePayDeskDto> _updatePayDeskValidator;

    /// <summary>
    /// Initializes a new instance of the <see cref="PayDeskService"/> class.
    /// </summary>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="mapper">The mapper.</param>
    /// <param name="addPayDeskValidator">The validator for adding a pay desk.</param>
    /// <param name="updatePayDeskValidator">The validator for updating a pay desk.</param>
    public PayDeskService(
        IUnitOfWork unitOfWork,
        ILogger<PayDeskService> logger,
        IMapper mapper,
        IValidator<AddPayDeskDto> addPayDeskValidator,
        IValidator<UpdatePayDeskDto> updatePayDeskValidator)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _addPayDeskValidator = addPayDeskValidator;
        _updatePayDeskValidator = updatePayDeskValidator;
    }

    /// <summary>
    /// Creates a new pay desk.
    /// </summary>
    /// <param name="payDesk">The pay desk DTO.</param>
    /// <returns>A tuple indicating success and the ID of the created pay desk.</returns>
    public async Task<(bool, Guid)> CreatePayDesk(AddPayDeskDto payDesk)
    {
        try
        {
            // Validate the pay desk DTO
            var validationResult = _addPayDeskValidator.Validate(payDesk);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            // Map DTO to PayDesk entity
            var newPayDesk = _mapper.Map<PayDesk>(payDesk);
            newPayDesk.Id = Guid.NewGuid();
            newPayDesk.CreatedAt = DateTime.UtcNow;

            // Add pay desk to the repository
            var isCreated = await _unitOfWork.PayDeskWrite.AddAsync(newPayDesk);

            if (!isCreated)
            {
                _logger.LogError("Pay desk creation failed for {Id}.", newPayDesk.Id);
                return (false, Guid.Empty);
            }

            _logger.LogInformation("Pay desk {PayDeskId} created successfully.", newPayDesk.Id);
            return (true, newPayDesk.Id);
        }
        catch (ValidationException ex)
        {
            _logger.LogWarning("Pay desk creation failed due to validation errors: {Errors}", ex.Errors);
            return (false, Guid.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the pay desk.");
            return (false, Guid.Empty);
        }
    }

    /// <summary>
    /// Updates an existing pay desk.
    /// </summary>
    /// <param name="payDesk">The pay desk DTO.</param>
    public async Task UpdatePayDesk(UpdatePayDeskDto payDesk)
    {
        try
        {
            var validationResult = _updatePayDeskValidator.Validate(payDesk);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingPayDesk = await _unitOfWork.PayDeskRead.GetByIdAsync(payDesk.Id);
            if (existingPayDesk == null)
            {
                _logger.LogWarning("Pay desk with ID {PayDeskId} not found.", payDesk.Id);
                throw new KeyNotFoundException($"Pay desk with ID {payDesk.Id} not found.");
            }

            // Map updated properties from DTO to entity
            _mapper.Map(payDesk, existingPayDesk);

            var isUpdated = _unitOfWork.PayDeskWrite.Update(existingPayDesk);

            if (!isUpdated)
            {
                _logger.LogError("Pay desk update failed for {PayDeskId}.", payDesk.Id);
                throw new Exception($"Pay desk update failed for {payDesk.Id}");
            }

            _logger.LogInformation("Pay desk {PayDeskId} updated successfully.", payDesk.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while updating pay desk {PayDeskId}.", payDesk.Id);
            throw;
        }
    }

    /// <summary>
    /// Deletes a pay desk by ID.
    /// </summary>
    /// <param name="id">The pay desk ID.</param>
    public async Task DeletePayDesk(Guid id)
    {
        try
        {
            var payDesk = await _unitOfWork.PayDeskRead.GetByIdAsync(id);
            if (payDesk == null)
            {
                _logger.LogWarning("Pay desk with ID {PayDeskId} not found.", id);
                throw new KeyNotFoundException($"Pay desk with ID {id} not found.");
            }

            _unitOfWork.PayDeskWrite.Delete(payDesk);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation("Pay desk {PayDeskId} deleted successfully.", id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting pay desk with ID {PayDeskId}.", id);
            throw;
        }
    }

    /// <summary>
    /// Gets all pay desks with sorting and filtering options.
    /// </summary>
    /// <param name="options">The sorting and filtering options.</param>
    /// <returns>A paged result of pay desk DTOs.</returns>
    public PagedResult<PayDeskDto> GetAllPayDesks(PayDeskSortFilterOptions options)
    {
        var payDesks = _unitOfWork.PayDeskRead.GetAll().AsNoTracking();
        var payDeskDtos = payDesks.Select(pd => _mapper.Map<PayDeskDto>(pd)).AsPagedResult(options);
        return payDeskDtos;
    }

    /// <summary>
    /// Gets a pay desk by ID.
    /// </summary>
    /// <param name="id">The pay desk ID.</param>
    /// <returns>The pay desk DTO.</returns>
    public async Task<PayDeskDto> GetPayDeskById(Guid id)
    {
        var payDesk = await _unitOfWork.PayDeskRead.GetByIdAsync(id);
        if (payDesk == null)
        {
            _logger.LogWarning("Pay desk with ID {PayDeskId} not found.", id);
            throw new KeyNotFoundException($"Pay desk with ID {id} not found.");
        }

        return _mapper.Map<PayDeskDto>(payDesk);
    }
}