using VehicleInventory.Application.Services;
using VehicleInventory.Infrastructure.DependencyInjection;
using VehicleInventory.Domain.Exceptions;
using VehicleInventory.Application.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Controllers (because your template might be minimal API)
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DI
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<VehicleService>();

var app = builder.Build();

// simple global exception handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (NotFoundException ex)
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (ConflictException ex)
    {
        context.Response.StatusCode = 409;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
    catch (DomainException ex)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsJsonAsync(new { error = ex.Message });
    }
});

// Swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.Redirect("/swagger"));
app.UseHttpsRedirection();

app.MapControllers();

app.Run();