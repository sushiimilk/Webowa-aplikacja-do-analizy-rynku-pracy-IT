using Microsoft.EntityFrameworkCore;
using RynekIT.Application.Abstractions;
using RynekIT.Application.Common;

namespace RynekIT.Application.Offers.SearchOffers;

public sealed class SearchOffersHandler(IAppDbContext db)
{
    public async Task<PagedResult<OfferListItem>> HandleAsync(SearchOffersQuery query, CancellationToken cancellationToken)
    {
        var page = Math.Max(query.Page, 1);
        var pageSize = Math.Clamp(query.PageSize, 1, SearchOffersQuery.MaxPageSize);

        var offers = db.Offers.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(query.Phrase))
        {
            var phrase = query.Phrase.Trim();
            offers = offers.Where(o => o.Title.Contains(phrase) || o.Company.Name.Contains(phrase));
        }

        if (query.WorkMode is not null)
        {
            offers = offers.Where(o => o.WorkMode == query.WorkMode);
        }

        var totalCount = await offers.CountAsync(cancellationToken);

        var items = await offers
            .OrderByDescending(o => o.PublishedAt)
            .ThenByDescending(o => o.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new OfferListItem(
                o.Id,
                o.Title,
                o.Company.Name,
                o.Location,
                o.WorkMode,
                o.SalaryMin,
                o.SalaryMax,
                o.SalaryCurrency,
                o.SourceUrl,
                o.PublishedAt))
            .ToListAsync(cancellationToken);

        return new PagedResult<OfferListItem>(items, page, pageSize, totalCount);
    }
}
