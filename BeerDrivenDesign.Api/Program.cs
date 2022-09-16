using BeerDrivenDesign.Api.Shared.Concretes;
using BeerDrivenDesign.Api.Shared.Configuration;
using BeerDrivenDesign.Modules.Produzione.Abstracts;
using BeerDrivenDesign.Modules.Produzione.Concretes;
using BeerDrivenDesign.Modules.Produzione.Endpoints;
using BeerDrivenDesign.Modules.Produzione.Shared.Validators;
using BeerDrivenDesign.ReadModel.MongoDb;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
#region Shared
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", corsBuilder =>
        corsBuilder.SetIsOriginAllowed(origin => true)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin());
});

builder.Services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs\\BeerDriven.log")
    .CreateLogger();

var mongoDbSettings = new MongoDbSettings();
builder.Configuration.GetSection("BrewUp:MongoDbSettings").Bind(mongoDbSettings);
builder.Services.AddMongoDb(mongoDbSettings);
#endregion

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => setup.SwaggerDoc("v1", new OpenApiInfo()
{
    Description = "BrewUp API",
    Title = "BrewUp Api",
    Version = "v1",
    Contact = new OpenApiContact
    {
        Name = "BrewUp.Api"
    }
}));
#endregion

#region Production
builder.Services.AddScoped<ValidationHandler>();
builder.Services.AddFluentValidation(options =>
    options.RegisterValidatorsFromAssemblyContaining<ProductionBeerValidator>());

builder.Services.AddScoped<IProductionOrchestrator, ProductionOrchestrator>();

builder.Services.AddScoped<IProductionService, ProductionService>();
builder.Services.AddScoped<IBeerService, BeerService>();
#endregion

var app = builder.Build();

app.UseCors("CorsPolicy");

// Map endpoints
#region Production
app.MapPost("v1/production/beers/brew", ProductionEndpoints.HandleStartProduction)
    .WithTags("Production");
app.MapPut("v1/production/beers/brew/{productionNumber}", ProductionEndpoints.HandleCompleteProduction)
    .WithTags("Production");

app.MapGet("v1/production/beers", ProductionEndpoints.HandleGetBeers)
    .WithTags("Production");
app.MapGet("v1/production", ProductionEndpoints.HandleGetProductionOrders)
    .WithTags("Production");
#endregion

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseSwagger(s =>
    {
        s.RouteTemplate = "documentation/{documentName}/documentation.json";
    });
    app.UseSwaggerUI(s =>
    {
        s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp BeerDrivenDesign");
        s.RoutePrefix = "documentation";
    });
}

app.Run();