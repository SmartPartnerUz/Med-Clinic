﻿namespace MedClinic.Domain.Models.Pagination;

public interface IPageOptions
{
    int Page { get; }
    int PageSize { get; }
}