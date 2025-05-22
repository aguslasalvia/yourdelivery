using Core.Entities;
using Core.Enums;

namespace DTO;

public class CreateUrgentShippingDto
{
    public int Id { get; set; }
    public string Tracking { get; set; }
    public float Weight { get; set; }
    public int Employee { get; set; }
    public int Client { get; set; }
    public ShippingState State { get; set; }
    public string Address { get; set; }
    
    public CreateUrgentShippingDto(){} 
    
    public CreateUrgentShippingDto(UrgentShipping shipping)
    {
        Id = shipping.Id;
        Weight = shipping.Weight;
        Employee = shipping.EmployeeID;
        Client = shipping.ClientID;
        State = shipping.State;
        Address = shipping.Address;
    }

    public UrgentShipping toUrgentShipping(){
        UrgentShipping shipping = new(){
            Id = this.Id,
            Weight = this.Weight,
            EmployeeID = this.Employee,
            ClientID = this.Client,
            State = this.State,
            Address = this.Address
        };
        
        return shipping;
    }
}