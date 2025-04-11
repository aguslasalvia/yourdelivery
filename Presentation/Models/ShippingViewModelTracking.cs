using Core.Enums;

namespace Presentation.Models;

public class ShippingViewModelTracking
{
    // TODO: Double-check this. used "int?" in the model rather than int because we used "int?" in the controller
    // TODO: and "ShippingState?" so we can send null to the view while we get the full backend to work
    public int? TrackingNumber { get; set; }
    public ShippingState? State { get; set; }
}