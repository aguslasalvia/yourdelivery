using Core.Enums;
using DTO;

namespace Application.Interfaces;

public interface IShippingGetByCommentary
{
    List<ShippingDto> Execute(string word, int userId);
    
}