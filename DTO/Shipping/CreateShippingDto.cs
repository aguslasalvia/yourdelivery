
namespace DTO;

public class CreateShippingDto
{
    public string ClientEmail { get; set; }

    // Urgent Shipping
    public string? Address { get; set; }
    
    //  Common Shipping
    public int PickupId { get; set; }
    public float Weight { get; set; }
    public int ClientId { get; set; }

    public string Type { get; set; }
    
    public CreateShippingDto(){}
    
    public CreateShippingDto(string? address, int pickupId, int clientId,  float weight, string type)
    {
        Address = address;
        PickupId = pickupId;
        ClientId = clientId;
        Weight = weight;
        Type = type;
    }
}