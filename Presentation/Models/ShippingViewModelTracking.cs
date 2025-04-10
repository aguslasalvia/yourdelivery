using Core.Enums;

namespace Presentation.Models;

public class ShippingViewModelTracking
{
    public int TrackingNumber { get; set; }
    public ShippingState State { get; set; }
}