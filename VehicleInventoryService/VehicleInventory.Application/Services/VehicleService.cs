using VehicleInventory.Application.DTOs;
using VehicleInventory.Application.Exceptions;
using VehicleInventory.Application.Interfaces;
using VehicleInventory.Domain.Entities;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.Services;

public class VehicleService
{
    private readonly IVehicleRepository _repo;

    public VehicleService(IVehicleRepository repo)
    {
        _repo = repo;
    }

    public async Task<VehicleResponse> CreateVehicleAsync(CreateVehicleRequest request)
    {
        if (await _repo.VehicleCodeExistsAsync(request.VehicleCode))
            throw new ConflictException("VehicleCode already exists.");

        var vehicle = new Vehicle(request.VehicleCode, request.LocationId, request.VehicleType);
        await _repo.AddAsync(vehicle);

        return Map(vehicle);
    }

    public async Task<VehicleResponse> GetVehicleByIdAsync(int id)
    {
        var vehicle = await _repo.GetByIdAsync(id);
        if (vehicle is null)
            throw new NotFoundException("Vehicle not found.");

        return Map(vehicle);
    }

    public async Task<List<VehicleResponse>> GetAllVehiclesAsync()
    {
        var vehicles = await _repo.GetAllAsync();
        return vehicles.Select(Map).ToList();
    }

    public async Task<VehicleResponse> UpdateVehicleStatusAsync(int id, UpdateVehicleStatusRequest request)
    {
        var vehicle = await _repo.GetByIdAsync(id);
        if (vehicle is null)
            throw new NotFoundException("Vehicle not found.");

        // IMPORTANT: call domain methods; do not set Status directly
        switch (request.Status)
        {
            case VehicleStatus.Available:
                vehicle.MarkAvailable(request.ExplicitRelease);
                break;
            case VehicleStatus.Reserved:
                vehicle.MarkReserved();
                break;
            case VehicleStatus.Rented:
                vehicle.MarkRented();
                break;
            case VehicleStatus.Serviced:
                vehicle.MarkServiced();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await _repo.UpdateAsync(vehicle);
        return Map(vehicle);
    }

    public async Task DeleteVehicleAsync(int id)
    {
        var vehicle = await _repo.GetByIdAsync(id);
        if (vehicle is null)
            throw new NotFoundException("Vehicle not found.");

        await _repo.DeleteAsync(vehicle);
    }

    private static VehicleResponse Map(Vehicle v) => new()
    {
        Id = v.Id,
        VehicleCode = v.VehicleCode,
        LocationId = v.LocationId,
        VehicleType = v.VehicleType,
        Status = v.Status
    };
}