using DTO;
using DTO.Users;

namespace Presentation.Models;

public class ShippingViewModelNew
{
  public List<UserListDto> Clients { get; set; }
  public List<AgencyShippingDto> Agencies { get; set; }
  public string Message { get; set; }
}