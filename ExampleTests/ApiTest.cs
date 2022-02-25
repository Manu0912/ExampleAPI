using System.Collections.Generic;
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
using Moq;
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
    public async Task Post_Returns_Created()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var response = await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() { IsCompleted =true, Name = "aaaaa" });
        await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() { IsCompleted = true, Name = "aaaaa" });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Put_Returns_NoContent()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() {Id = 5, IsCompleted = true, Name = "aaaaa" });
        var response = await client.PutAsJsonAsync("/api/ExampleItems/5", new ExampleItem() { Id = 5, IsCompleted = true, Name = "aaaaa" });
        
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Put_Returns_BadRequest()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var response = await client.PutAsJsonAsync("/api/ExampleItems/5", new ExampleItem() { Id = 1, IsCompleted = true, Name = "aaaaa" });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task GetById_Returns_OK()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() {Id = 20, IsCompleted = true, Name = "aaaaa" });
        var response = await client.GetAsync("/api/ExampleItems/20");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Returns_OK()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        await client.PostAsJsonAsync("/api/ExampleItems", new ExampleItem() { IsCompleted = true, Name = "aaaaa" });
        var response = await client.DeleteAsync("/api/ExampleItems/1");

        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_Returns_NotFound()
    {
        await using var application = new TodoApplication();
        var client = application.CreateClient();
        var response = await client.DeleteAsync("/api/ExampleItems/8");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
                services.AddControllers();

            });
            return base.CreateHost(builder);
        }
    }
}