using System.Text.Json.Serialization;
using EdenredApp.API;
using EdenredApp.Application.Interfaces;
using EdenredApp.Application.Services;
using EdenredApp.Infra.Model;
using EdenredApp.Persistance.Context;
using EdenredApp.Persistance.Interfaces;
using EdenredApp.Persistance.Models;
using EdenredApp.Persistance.Repository;
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
