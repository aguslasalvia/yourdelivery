using Core.Entities;
using Core.Interfaces;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ShippingRepository:IShippingRepository
{
    private readonly AppDbContext _context;

    public ShippingRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public Shipping Add(Shipping shipping)
    {
        throw new NotImplementedException();
    }

    public Shipping Update(Shipping shipping)
    {
        throw new NotImplementedException();
    }

    public Shipping Delete(Shipping shipping)
    {
        throw new NotImplementedException();
    }

    public Shipping? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Shipping> GetAll()
    {
        throw new NotImplementedException();
    }
}