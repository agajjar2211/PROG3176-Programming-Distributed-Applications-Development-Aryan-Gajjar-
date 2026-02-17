using System.ComponentModel.DataAnnotations;
using VehicleInventory.Domain.Enums;

namespace VehicleInventory.Application.DTOs;

public class UpdateVehicleStatusRequest
{
    [Required]
    public VehicleStatus Status { get; set; }

    // needed for "reserved -> available requires explicit release"
    public bool ExplicitRelease { get; set; } = false;
}