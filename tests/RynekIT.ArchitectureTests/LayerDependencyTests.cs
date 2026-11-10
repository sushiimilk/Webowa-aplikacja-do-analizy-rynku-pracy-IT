using System.Reflection;
using NetArchTest.Rules;
using RynekIT.Api.Endpoints;
using RynekIT.Application;
using RynekIT.Domain.Offers;
using RynekIT.Infrastructure;

namespace RynekIT.ArchitectureTests;

public class LayerDependencyTests
{
    private static readonly Assembly DomainAssembly = typeof(Offer).Assembly;
    private static readonly Assembly ApplicationAssembly = typeof(Application.DependencyInjection).Assembly;
    private static readonly Assembly InfrastructureAssembly = typeof(Infrastructure.DependencyInjection).Assembly;

    private const string ApplicationNamespace = "RynekIT.Application";
    private const string InfrastructureNamespace = "RynekIT.Infrastructure";
    private const string ApiNamespace = "RynekIT.Api";

    [Fact]
    public void Domain_DoesNotDependOnOtherLayers()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(ApplicationNamespace, InfrastructureNamespace, ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Domain_DoesNotDependOnFrameworks()
    {
        var result = Types.InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAny("Microsoft.EntityFrameworkCore", "Microsoft.AspNetCore")
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Application_DoesNotDependOnInfrastructureOrApi()
    {
        var result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(InfrastructureNamespace, ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Infrastructure_DoesNotDependOnApi()
    {
        var result = Types.InAssembly(InfrastructureAssembly)
            .ShouldNot()
            .HaveDependencyOn(ApiNamespace)
            .GetResult();

        AssertSuccessful(result);
    }

    [Fact]
    public void Endpoints_AreStaticClassesInEndpointsNamespace()
    {
        var result = Types.InAssembly(typeof(OffersEndpoints).Assembly)
            .That()
            .HaveNameEndingWith("Endpoints")
            .Should()
            .BeStatic()
            .And()
            .ResideInNamespace("RynekIT.Api.Endpoints")
            .GetResult();

        AssertSuccessful(result);
    }

    private static void AssertSuccessful(TestResult result)
    {
        var failing = result.FailingTypeNames ?? [];
        Assert.True(result.IsSuccessful, "Naruszenie zasad architektury: " + string.Join(", ", failing));
    }
}
