using System.Text.Json.Serialization;
using MobileCredits.API;
using MobileCredits.Application.Interfaces;
using MobileCredits.Application.Services;
using MobileCredits.Persistance.Context;
using MobileCredits.Persistance.Interfaces;
using MobileCredits.Persistance.Models;
using MobileCredits.Persistance.Repository;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.AddContext<AppJsonContext>();
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IBeneficiaryRepository, BeneficiaryRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddDbContext<EdenredAppContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseModel(EdenredAppContextModel.Instance);
});

var app = builder.Build();
Routes.RegisterRoutes(app);

app.Run();
