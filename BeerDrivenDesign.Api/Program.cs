using BeerDrivenDesign.Api.Modules;
using BeerDrivenDesign.Modules.Produzione.Hubs;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

// Register Modules
builder.RegisterModules();

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

var app = builder.Build();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapEndpoints();

app.MapHub<ProductionHub>("hubs/production");

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