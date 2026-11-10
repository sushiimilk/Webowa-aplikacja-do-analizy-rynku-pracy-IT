using RynekIT.Application.Offers.SearchOffers;
using RynekIT.Domain.Offers;

namespace RynekIT.Application.UnitTests.Offers;

public class SearchOffersHandlerTests
{
    private static async Task<TestDbContext> CreateDbWithOffersAsync()
    {
        var db = TestDbContext.Create();
        var company = new Company { Name = "DataForge" };

        db.Offers.AddRange(
            CreateOffer(company, "Junior Python Developer", WorkMode.Remote, new DateTime(2026, 11, 1)),
            CreateOffer(company, "Senior .NET Developer", WorkMode.Hybrid, new DateTime(2026, 11, 3)),
            CreateOffer(company, "Data Analyst", WorkMode.Onsite, new DateTime(2026, 11, 2)));

        await db.SaveChangesAsync();
        return db;
    }

    private static Offer CreateOffer(Company company, string title, WorkMode workMode, DateTime publishedAt) => new()
    {
        Company = company,
        Title = title,
        WorkMode = workMode,
        Source = "test",
        SourceOfferId = title,
        SourceUrl = "https://example.com",
        PublishedAt = publishedAt,
    };

    [Fact]
    public async Task WithoutFilters_ReturnsAllOffersNewestFirst()
    {
        await using var db = await CreateDbWithOffersAsync();

        var result = await new SearchOffersHandler(db).HandleAsync(new SearchOffersQuery(), CancellationToken.None);

        Assert.Equal(3, result.TotalCount);
        Assert.Equal(["Senior .NET Developer", "Data Analyst", "Junior Python Developer"], result.Items.Select(o => o.Title));
    }

    [Fact]
    public async Task Phrase_FiltersByTitle()
    {
        await using var db = await CreateDbWithOffersAsync();

        var result = await new SearchOffersHandler(db).HandleAsync(new SearchOffersQuery(Phrase: "Developer"), CancellationToken.None);

        Assert.Equal(2, result.TotalCount);
    }

    [Fact]
    public async Task WorkMode_FiltersByWorkMode()
    {
        await using var db = await CreateDbWithOffersAsync();

        var result = await new SearchOffersHandler(db).HandleAsync(new SearchOffersQuery(WorkMode: WorkMode.Remote), CancellationToken.None);

        var offer = Assert.Single(result.Items);
        Assert.Equal("Junior Python Developer", offer.Title);
    }

    [Fact]
    public async Task Paging_ReturnsRequestedPageAndTotalCount()
    {
        await using var db = await CreateDbWithOffersAsync();

        var result = await new SearchOffersHandler(db).HandleAsync(new SearchOffersQuery(Page: 2, PageSize: 2), CancellationToken.None);

        Assert.Equal(3, result.TotalCount);
        Assert.Equal(2, result.TotalPages);
        Assert.Equal("Junior Python Developer", Assert.Single(result.Items).Title);
    }

    [Fact]
    public async Task PageSize_IsLimitedToMaximum()
    {
        await using var db = await CreateDbWithOffersAsync();

        var result = await new SearchOffersHandler(db).HandleAsync(new SearchOffersQuery(PageSize: 1000), CancellationToken.None);

        Assert.Equal(SearchOffersQuery.MaxPageSize, result.PageSize);
    }
}
