
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShippingRepository(DbContext context) : IShippingRepository
    {

        private readonly DbContext _context = context;

        public Shipping Add(Shipping shipping)
        {
            _context.Set<Shipping>().Add(shipping);
            _context.SaveChanges();
            return shipping;
        }
        public Shipping Update(Shipping shipping)
        {
            _context.Entry(shipping).State = EntityState.Modified;
            _context.SaveChanges();
            return shipping;
        }

        public Shipping Delete(Shipping shipping)
        {
            _context.Remove(shipping);
            return shipping;
        }
        public Shipping? GetById(int id)
        {
            return _context.Set<Shipping>().FirstOrDefault(shipping => shipping.ID == id);

        }

        public IEnumerable<Shipping> GetAll()
        {
            return _context.Set<Shipping>().ToList();
        }
    }
}


