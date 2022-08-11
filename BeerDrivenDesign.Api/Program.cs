using BeerDrivenDesign.Api;
using BeerDrivenDesign.Api.Modules;
using BlazorChatSignalR.Server.Hubs;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Register Modules
//builder.RegisterModules();

builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", b =>
    {
        b.SetIsOriginAllowed(origin => true).AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials();
    });
});

builder.Services.AddSingleton<CounterService>();
builder.Services.AddSingleton<ChatHub>();
var app = builder.Build();

app.UseCors("CorsPolicy");


//app.UseAuthentication();
app.UseAuthorization();

// Register endpoints
app.MapEndpoints();

app.MapHub<ChatHub>("/chathub");

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    //app.UseSwagger(s =>
    //{
    //    s.RouteTemplate = "documentation/{documentName}/documentation.json";
    //});
    //app.UseSwaggerUI(s =>
    //{
    //    s.SwaggerEndpoint("/documentation/v1/documentation.json", "BrewUp BeerDrivenDesign");
    //    s.RoutePrefix = "documentation";
    //});
}

app.MapGet("/test", context =>
{
    context.RequestServices.GetService<CounterService>().StartCount();
    return Task.FromResult(0);
});

app.Run();