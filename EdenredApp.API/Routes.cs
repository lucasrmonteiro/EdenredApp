using EdenredApp.Application.Interfaces;
using EdenredApp.Infra.Model;
using Microsoft.OpenApi.Models;

namespace EdenredApp.API;

public static class Routes
{
    public static void RegisterRoutes(WebApplication app)
    {
        app.MapPost("/api/AddCredit", async (CreditModel credit, IUserService userService) =>
            {
                var result = await userService.AddCreditForBeneficiary(credit.UserId, credit.NickName, credit.Amount);
                return Results.Ok(result);
            })
            .WithName("AddCredit");

        app.MapGet("/api/GetBeneficiariesByUserId/{userId}", async (long userId, IUserService userService) =>
        {
            var result = await userService.GetBeneficiariesByUserId(userId);
            return Results.Ok(result);
        }).WithName("GetBeneficiariesByUserId");

        app.MapGet("/api/RefillsAvailable",
            (IUserService userService) => { return Results.Ok(userService.GetRefillsAvailable()); })
            .WithName("RefillsAvailable");
    }
}