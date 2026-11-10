using RynekIT.Domain.Offers;

namespace RynekIT.Application.Offers.SearchOffers;

public sealed record OfferListItem(
    int Id,
    string Title,
    string Company,
    string? Location,
    WorkMode? WorkMode,
    decimal? SalaryMin,
    decimal? SalaryMax,
    string? SalaryCurrency,
    string SourceUrl,
    DateTime? PublishedAt);
