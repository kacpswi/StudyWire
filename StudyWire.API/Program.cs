using StudyWire.Infrastructure.Extensions;
using StudyWire.Application.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Data;
using StudyWire.Infrastructure.Presistence;
using Microsoft.EntityFrameworkCore;
using StudyWire.Domain.Entities;
using StudyWire.Infrastructure.Seeders;
using StudyWire.API.Extensions;
using StudyWire.API.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddPresentationServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200", "https://localhost:4200"));

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

if (Environment.GetEnvironmentVariable("DOCKER_ENVIRONMENT") == "Docker")
{
    app.UseDefaultFiles();
    app.UseStaticFiles();

    app.MapFallbackToController("Index", "Fallback");
}


app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<StudyWireDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();
    await context.Database.MigrateAsync();
    await SchoolsSeeder.SeedSchools(context);
    await UsersSeeder.SeedUsers(userManager, roleManager, context);
    await NewsSeeder.SeedNews(context);
}
catch (Exception ex)
{
    var logger = services.GetService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
