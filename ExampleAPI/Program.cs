using Microsoft.EntityFrameworkCore;
using ExampleAPI.Models;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TestContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase"));
});

//builder.Services.AddDbContext<UserContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("TestDatabase"));
//});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Example API",
        Description = "An ASP.NET Core Web API for managing Example items",
        TermsOfService = new Uri("https://exampleterms.com"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://emanuelcacheda.herokuapp.com")
        },
    });;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();