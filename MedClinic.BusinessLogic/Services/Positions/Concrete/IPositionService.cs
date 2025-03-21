using MedClinic.Domain.Models.PageResult;

namespace MedClinic.BusinessLogic.Services;

/// <summary>
/// Interface for managing positions.
/// </summary>
public interface IPositionService
{
    Task<(bool, Guid)> CreatePosition(AddPositionDto position);
    Task UpdatePosition(UpdatePositionDto position);
    Task DeletePosition(Guid id);
    PagedResult<PositionDto> GetAllPositions(PositionSortFilterOptions options);
    Task<PositionDto> GetPositionById(Guid id);
}