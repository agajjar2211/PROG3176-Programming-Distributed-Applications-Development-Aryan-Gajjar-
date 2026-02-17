using VehicleInventory.Domain.Enums;
using VehicleInventory.Domain.Exceptions;

namespace VehicleInventory.Domain.Entities;

public class Vehicle
{
    public int Id { get; private set; }
    public string VehicleCode { get; private set; } = default!;
    public string LocationId { get; private set; } = default!;
    public string VehicleType { get; private set; } = default!;
    public VehicleStatus Status { get; private set; }

    private Vehicle() { } // EF

    public Vehicle(string vehicleCode, string locationId, string vehicleType)
    {
       
        SetVehicleCode(vehicleCode);
        SetLocationId(locationId);
        SetVehicleType(vehicleType);
        Status = VehicleStatus.Available;
    }

    private void SetVehicleCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new DomainException("VehicleCode is required.");
        if (code.Length > 20)
            throw new DomainException("VehicleCode must be <= 20 characters.");
        VehicleCode = code.Trim();
    }

    private void SetLocationId(string locationId)
    {
        if (string.IsNullOrWhiteSpace(locationId))
            throw new DomainException("LocationId is required.");
        if (locationId.Length > 50)
            throw new DomainException("LocationId must be <= 50 characters.");
        LocationId = locationId.Trim();
    }

    private void SetVehicleType(string type)
    {
        if (string.IsNullOrWhiteSpace(type))
            throw new DomainException("VehicleType is required.");
        if (type.Length > 50)
            throw new DomainException("VehicleType must be <= 50 characters.");
        VehicleType = type.Trim();
    }

    // Domain behaviors
    public void MarkAvailable(bool explicitRelease = false)
    {
        // Rule: reserved cannot be made available without explicit release
        if (Status == VehicleStatus.Reserved && !explicitRelease)
            throw new DomainException("Reserved vehicle cannot be marked Available without explicit release.");

        Status = VehicleStatus.Available;
    }

    public void MarkReserved()
    {
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Rented vehicle cannot be reserved.");
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("Vehicle under service cannot be reserved.");

        Status = VehicleStatus.Reserved;
    }

    public void MarkRented()
    {
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Vehicle is already rented.");
        if (Status == VehicleStatus.Reserved)
            throw new DomainException("Reserved vehicle cannot be rented.");
        if (Status == VehicleStatus.Serviced)
            throw new DomainException("Vehicle under service cannot be rented.");

        Status = VehicleStatus.Rented;
    }

    public void MarkServiced()
    {
        if (Status == VehicleStatus.Rented)
            throw new DomainException("Rented vehicle cannot be put under service.");

        Status = VehicleStatus.Serviced;
    }
}