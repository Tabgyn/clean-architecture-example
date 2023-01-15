using CleanArchitectureExample.Domain.Common.Converters;
using CleanArchitectureExample.Infrastructure;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ProjectBootstrap(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters
        .Add(new DateOnlyJsonConverter()));

builder.Services.AddSwaggerGen(options =>
    options.MapType<DateOnly>(() => new OpenApiSchema
    {
        Type = "string",
        Format = "date",
        Example = new OpenApiString("1900-01-01")
    }));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
