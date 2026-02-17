using Microsoft.EntityFrameworkCore;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Infrastructure.Persistence;

namespace VehicleInventory.Infrastructure.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly InventoryDbContext _db;

    public VehicleRepository(InventoryDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        _db.Vehicles.Add(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Vehicle vehicle)
    {
        _db.Vehicles.Remove(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Vehicle>> GetAllAsync()
    {
        return await _db.Vehicles.AsNoTracking().ToListAsync();
    }

    public async Task<Vehicle?> GetByIdAsync(int id)
    {
        return await _db.Vehicles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Vehicle vehicle)
    {
        _db.Vehicles.Update(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> VehicleCodeExistsAsync(string vehicleCode)
    {
        return await _db.Vehicles.AnyAsync(x => x.VehicleCode == vehicleCode);
    }
}