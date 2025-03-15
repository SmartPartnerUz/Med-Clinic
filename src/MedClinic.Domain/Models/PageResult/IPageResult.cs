namespace MedClinic.Domain.Models.PageResult;

public interface IPagedResult
{
    int Page { get; }
    int PageSize { get; }
    long Total { get; }
}