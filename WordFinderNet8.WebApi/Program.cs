using Asp.Versioning.ApiExplorer;
using WordFinder.Application.UseCases;
using WordFinder.Infrastructure;
using WordFinderNet8.WebApi.Extensions.Middleware;
using WordFinderNet8.WebApi.Extensions.Swagger;
using WordFinderNet8.WebApi.Extensions.Versioning;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Methods Extensions
builder.Services.AddInjectionPersistence();
builder.Services.AddInjectionApplication();

builder.Services.AddVersioning();
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToLowerInvariant());
    }
});

app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.AddMiddleware();

app.MapControllers();

app.Run();
