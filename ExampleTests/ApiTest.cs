using System.Net;
using System.Threading.Tasks;
using ExampleAPI.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace TodoDapper.Tests;

public class SmokeTests
{
    [Fact]
    public async Task GetTodos_Returns_OK()
    {
        await using var application = new TodoApplication();

        var client = application.CreateClient();
        var response = await client.GetAsync("/api/ExampleItems");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
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
            });

            return base.CreateHost(builder);
        }
    }
}