using Core.Entities;
using Core.Enums;
using Core.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ShippingRepository : IShippingRepository
{
	private readonly AppDbContext _context;

	public ShippingRepository(AppDbContext context)
	{
		_context = context;
	}

	public Shipping Add(Shipping shipping)
	{
		if (shipping.Weight <= 0)
			throw new Exception("Weight can't be negative or zero");

		_context.Shippings.Add(shipping);
		_context.SaveChanges();
		return shipping;
	}

	public Shipping Update(Shipping shipping)
	{
		_context.Shippings.Update(shipping);
		_context.SaveChanges();
		return shipping;
	}

	public Shipping Delete(Shipping shipping)
	{
		throw new NotImplementedException();
	}

	public Shipping GetById(int id)
	{
		Shipping shipping = _context.Shippings
				.Include(s => s.Client)
				.Include(s => s.Employee)
				.FirstOrDefault(s => s.Id == id);
		// KeyNotFoundException: the exception that is thrown when the key specified
		// for accessing an element in a collection does not match any key in the collection.
		// Figured it fit better than ArgumentException
		return shipping ?? throw new KeyNotFoundException($"Shipping with ID {id} was not found.");
	}

	public IEnumerable<Shipping> GetByClientId(int clientId)
	{
		return _context.Shippings

			.Where(s => s.ClientID == clientId
									&& s.State == ShippingState.OnProcess)
			.Include(s => s.Client)
			.Include(s => s.Employee)

			.ToList();
	}

	public IEnumerable<Shipping> GetAllOnProcess()
	{
		return _context.Shippings
			.Include(s => s.Client)
			.Include(s => s.Employee)
			.Where(s => s.State == ShippingState.OnProcess)
			.ToList();
	}
}