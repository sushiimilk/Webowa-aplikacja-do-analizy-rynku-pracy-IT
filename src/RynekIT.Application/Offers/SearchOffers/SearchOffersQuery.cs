using RynekIT.Domain.Offers;

namespace RynekIT.Application.Offers.SearchOffers;

public sealed record SearchOffersQuery(
    string? Phrase = null,
    WorkMode? WorkMode = null,
    int Page = 1,
    int PageSize = 20)
{
    public const int MaxPageSize = 100;
}
