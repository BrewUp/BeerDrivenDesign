using BeerDrivenDesign.Modules.Pubs.Abstracts;
using Microsoft.AspNetCore.Http;

namespace BeerDrivenDesign.Modules.Pubs.Endpoints;

public static class PubsEndpoints
{
    public async static Task<IResult> HandleGetBeers(IPubsService pubsService)
    {
        return Results.Ok();
    }
}