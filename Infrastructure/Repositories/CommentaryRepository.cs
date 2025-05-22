namespace Infrastructure.Repositories;

using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;

public class CommentaryRepository : ICommentaryRepository
{
	private readonly AppDbContext _context;

	public CommentaryRepository(AppDbContext context)
	{
		_context = context;
	}

	public Commentary Add(Commentary commentary)
	{
		_context.Commentaries.Add(commentary);
		_context.SaveChanges();
		return commentary;
	}

	public Commentary Delete(Commentary commentary)
	{
		_context.Commentaries.Remove(commentary);
		_context.SaveChanges();
		return commentary;
	}

	public Commentary GetByTrackingId(int id)
	{
		return _context.Commentaries.FirstOrDefault(c => c.ShippingId == id);
	}

	public IEnumerable<Commentary> GetAll()
	{
		return _context.Commentaries.ToList();
	}
	public Commentary Update(Commentary commentary)
	{
		_context.Commentaries.Update(commentary);
		_context.SaveChanges();
		return commentary;
	}
}