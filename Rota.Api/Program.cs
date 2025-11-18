using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;

using Rota.Api.Data;
using Rota.Api.Domain;
using Rota.Api.Dtos;
using Rota.Api.Validators;
using Rota.Api.Services;

var builder = WebApplication.CreateBuilder(args);

#region Configuração da Aplicação

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRouteRequestValidator>();

#endregion

#region Configuração dos Serviços (Injeção de Dependência)

builder.Services.AddScoped<IDistanceService, DistanceService>();
builder.Services.AddScoped<IRouteCalculator, RouteCalculator>();

#endregion

#region Configuração do EF Core

if (!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<RotaDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}

#endregion

#region Configuração do JSON

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNameCaseInsensitive = true;
});

#endregion

var app = builder.Build();

#region Configuração do Swagger

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#endregion

app.UseHttpsRedirection();

#region Endpoint Inicial

app.MapGet("/", () => Results.Ok(new { status = "API de Rotas Inteligente Ativa" }));

#endregion

#region Endpoints de Veículos

var vehicleGroup = app.MapGroup("/vehicles");

vehicleGroup.MapGet("/", async (RotaDbContext db) =>
{
    return await db.Vehicles.ToListAsync();
});

vehicleGroup.MapGet("/{id:int}", async (int id, RotaDbContext db) =>
{
    var vehicle = await db.Vehicles.FindAsync(id);
    return vehicle is null ? Results.NotFound() : Results.Ok(vehicle);
});

vehicleGroup.MapPost("/", async (CreateVehicleDto dto, RotaDbContext db) =>
{
    var vehicle = new Vehicle
    {
        Name = dto.Name,
        MaxWeightKg = dto.MaxWeightKg,
        MaxVolumeM3 = dto.MaxVolumeM3,
        MaxLoadWeightKg = dto.MaxLoadWeightKg,
        CostPerKm = dto.CostPerKm,
        CostPerHour = dto.CostPerHour,
        DriverCostPerHour = dto.DriverCostPerHour,
        MaxDistanceWithoutRefuelKm = dto.MaxDistanceWithoutRefuelKm,
        MaxHeightM = dto.MaxHeightM,
        VehicleType = dto.VehicleType
    };

    db.Vehicles.Add(vehicle);
    await db.SaveChangesAsync();

    return Results.Created($"/vehicles/{vehicle.Id}", vehicle);
});

vehicleGroup.MapPut("/{id:int}", async (int id, UpdateVehicleDto dto, RotaDbContext db) =>
{
    var v = await db.Vehicles.FindAsync(id);
    if (v is null) return Results.NotFound();

    v.Name = dto.Name;
    v.MaxWeightKg = dto.MaxWeightKg;
    v.MaxVolumeM3 = dto.MaxVolumeM3;
    v.MaxLoadWeightKg = dto.MaxLoadWeightKg;
    v.CostPerKm = dto.CostPerKm;
    v.CostPerHour = dto.CostPerHour;
    v.DriverCostPerHour = dto.DriverCostPerHour;
    v.MaxDistanceWithoutRefuelKm = dto.MaxDistanceWithoutRefuelKm;
    v.MaxHeightM = dto.MaxHeightM;
    v.VehicleType = dto.VehicleType;

    await db.SaveChangesAsync();

    return Results.Ok(v);
});

vehicleGroup.MapDelete("/{id:int}", async (int id, RotaDbContext db) =>
{
    var v = await db.Vehicles.FindAsync(id);
    if (v is null) return Results.NotFound();

    db.Vehicles.Remove(v);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

#endregion

#region Endpoints de Rotas

var routeGroup = app.MapGroup("/route-requests");

routeGroup.MapGet("/", async (RotaDbContext db) =>
{
    return await db.RouteRequests
        .Include(r => r.Waypoints)
        .Include(r => r.Vehicle)
        .ToListAsync();
});

routeGroup.MapGet("/{id:int}", async (int id, RotaDbContext db) =>
{
    var req = await db.RouteRequests
        .Include(r => r.Waypoints)
        .Include(r => r.Vehicle)
        .Include(r => r.Results)
        .FirstOrDefaultAsync(r => r.Id == id);

    return req is null ? Results.NotFound() : Results.Ok(req);
});

routeGroup.MapPost("/", async (CreateRouteRequestDto dto, RotaDbContext db, IValidator<CreateRouteRequestDto> validator) =>
{
    var validation = await validator.ValidateAsync(dto);
    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);

    var request = new RouteRequest
    {
        Name = dto.Name,
        Optimization = dto.Optimization,
        TotalLoadWeightKg = dto.TotalLoadWeightKg,
        TotalLoadVolumeM3 = dto.TotalLoadVolumeM3,
        VehicleId = dto.VehicleId,
        Waypoints = dto.Waypoints.Select(w => new Waypoint
        {
            Order = w.Order,
            Latitude = w.Latitude,
            Longitude = w.Longitude
        }).ToList()
    };

    db.RouteRequests.Add(request);
    await db.SaveChangesAsync();

    return Results.Created($"/route-requests/{request.Id}", request);
});

routeGroup.MapPut("/{id:int}", async (int id, UpdateRouteRequestDto dto, RotaDbContext db, IValidator<UpdateRouteRequestDto> validator) =>
{
    var validation = await validator.ValidateAsync(dto);
    if (!validation.IsValid)
        return Results.BadRequest(validation.Errors);

    var existing = await db.RouteRequests
        .Include(r => r.Waypoints)
        .FirstOrDefaultAsync(r => r.Id == id);

    if (existing is null)
        return Results.NotFound();

    existing.Name = dto.Name;
    existing.Optimization = dto.Optimization;
    existing.TotalLoadWeightKg = dto.TotalLoadWeightKg;
    existing.TotalLoadVolumeM3 = dto.TotalLoadVolumeM3;
    existing.VehicleId = dto.VehicleId;

    existing.Waypoints = dto.Waypoints.Select(w => new Waypoint
    {
        Order = w.Order,
        Latitude = w.Latitude,
        Longitude = w.Longitude,
        RouteRequestId = existing.Id
    }).ToList();

    await db.SaveChangesAsync();

    return Results.Ok(existing);
});

routeGroup.MapDelete("/{id:int}", async (int id, RotaDbContext db) =>
{
    var req = await db.RouteRequests.FindAsync(id);
    if (req is null)
        return Results.NotFound();

    db.RouteRequests.Remove(req);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

#endregion

#region Cálculo da Rota (USANDO IRouteCalculator)

routeGroup.MapPost("/{id:int}/calculate", async (int id, RotaDbContext db, IRouteCalculator calculator) =>
{
    var req = await db.RouteRequests
        .Include(r => r.Waypoints)
        .Include(r => r.Vehicle)
        .FirstOrDefaultAsync(r => r.Id == id);

    if (req is null) return Results.NotFound();

    try
    {
        var result = calculator.Calculate(req);

        db.RouteResults.Add(result);
        await db.SaveChangesAsync();

        return Results.Ok(result);
    }
    catch (RouteCalculationException ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

#endregion

#region Endpoints de Resultados

var resultGroup = app.MapGroup("/route-results");

resultGroup.MapGet("/", async (RotaDbContext db) =>
{
    return await db.RouteResults
        .Include(r => r.RouteRequest)
        .ToListAsync();
});

resultGroup.MapGet("/{id:int}", async (int id, RotaDbContext db) =>
{
    var r = await db.RouteResults
        .Include(rr => rr.RouteRequest)
        .FirstOrDefaultAsync(rr => rr.Id == id);

    return r is null ? Results.NotFound() : Results.Ok(r);
});

#endregion

app.Run();

public partial class Program { }
