using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;
public class AgencyRepository : IAgencyRepository
{
	private readonly AppDbContext _context;

	public AgencyRepository(AppDbContext context)
	{
		_context = context;
	}

	public Agency Add(Agency agency)
	{
		throw new NotImplementedException();
	}

	public Agency Delete(Agency agency)
	{
		throw new NotImplementedException();
	}

	public IEnumerable<Agency> GetAll()
	{
		return _context.Agencies.ToList();
	}

	public Agency? GetByName(string name)
	{
		throw new NotImplementedException();
	}

	public Agency Update(Agency agency)
	{
		throw new NotImplementedException();
	}

	public Agency? GetById(int id)
	{
		return _context.Agencies.FirstOrDefault(a => a.Id == id);
	} 
}
