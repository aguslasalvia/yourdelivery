using DTO;

namespace Application.Interfaces;

public interface IShippingCreate
{
	void ExecuteCommon(CreateCommonShippingDto createCommonShipping);

	void ExecuteUrgent(CreateUrgentShippingDto createUrgentShipping);



}