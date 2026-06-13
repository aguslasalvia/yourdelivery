using  Infrastructure.Persistence;
using  Core.Entities;
using  Core.Enums;
using  Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace  Infrastructure.Repositories;

public class ShippingRepository : IShippingRepository
{
	private readonly AppDbContext _context;

	public ShippingRepository(AppDbContext context)
	{
		_context = context;
	}

	public Shipping Add(Shipping shipping)
	{
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

	public IEnumerable<Shipping> GetByDates(DateTime from, DateTime to, ShippingState state, int userId)
	{
		return _context.Shippings
			.Include(s => s.Client)
			.Include(s => s.Employee)
			.Where(s => s.CreatedOn >= from && s.CreatedOn <= to && s.State == state && s.Client.Id == userId)
			.ToList();
	}
	public Shipping GetById(int id)
	{
		Shipping shipping = _context.Shippings
				.Include(s => s.Client)
				.Include(s => s.Employee)
				.FirstOrDefault(s => s.Id == id);
		return shipping ?? throw new KeyNotFoundException($"Shipping with ID {id} was not found.");
	}
	
	public IEnumerable<Shipping> GetByCommentary(string word, int userId)
	{
		return _context.Shippings
			.Include(s => s.Client)
			.Include(s => s.Employee)
			.Where(s => s.ClientID == userId)
			.Where(s => _context.Commentaries
				.Any(c => c.ShippingId == s.Id && c.Text.Contains(word)))
			.OrderByDescending(s => s.CreatedOn)
			.ToList();
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