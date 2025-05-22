using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Enums;

namespace DTO;

public class CreateCommonShippingDto
{
    public int Id { get; set; }
    public string Tracking { get; set; }
    public float Weight { get; set; }
    public int EmployeeId { get; set; }
    public int ClientId { get; set; }
    public ShippingState State { get; set; }
    
    // Common shipping
    public int PickupId { get; set; }

    public CreateCommonShippingDto(){} 
    
    public CreateCommonShippingDto(CommonShipping shipping)
    {
        Id = shipping.Id;
        Weight = shipping.Weight;
        EmployeeId = shipping.EmployeeID;
        ClientId = shipping.ClientID;
        State = shipping.State;
        PickupId = shipping.PickupId;
    }

    public CommonShipping toCommonShipping(){
        CommonShipping shipping = new(){
            Id = this.Id,
            Weight = this.Weight,
            EmployeeID = this.EmployeeId,
            ClientID = this.ClientId,
            State = this.State,
            PickupId = this.PickupId
        };
        
        return shipping;
    }
}