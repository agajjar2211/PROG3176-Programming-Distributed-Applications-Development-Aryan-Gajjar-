using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs;

public class VehicleResponse
{
    public int Id { get; set; }
    public string VehicleCode { get; set; } = default!;
    public string LocationId { get; set; } = default!;
    public string VehicleType { get; set; } = default!;
    public VehicleStatus Status { get; set; }
}