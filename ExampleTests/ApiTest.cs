using System.Net;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using ExampleAPI.Models;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace ExampleTests.ApiTest;

public class SmokeTests
{
    [Fact]
    public async Task GetAll_Returns_OK()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.GetAsync("/api/ExampleItems");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Post_Returns_OK()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() { IsCompleted =true, Name = "aaaaa"});
        
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Post_Returns_Error()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() { Name = "aaaaa" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }


    class TodoApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            // Add mock/test services to the builder here
            builder.ConfigureServices(services =>
            {
                services.AddDbContext<TestContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryExampleTest");
                });
                services.AddControllers()
                .AddFluentValidation(options =>
                {
                    // Validate child properties and root collection elements
                    options.ImplicitlyValidateChildProperties = true;
                    options.ImplicitlyValidateRootCollectionElements = true;
                    // Automatic registration of validators in assembly
                    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });
            });

            return base.CreateHost(builder);
        }
    }
}