
using BusinessLogic.Domain;
using BusinessLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class AgencyRepository(DbContext context) : IAgencyRepository
    {

        private readonly DbContext _context = context;

        public Agency Add(Agency agency)
        {
            _context.Set<Agency>().Add(agency);
            _context.SaveChanges();
            return agency;
        }
        public Agency Update(Agency agency)
        {
            _context.Entry(agency).State = EntityState.Modified;
            _context.SaveChanges();
            return agency;
        }

        public Agency Delete(Agency agency)
        {
            _context.Remove(agency);
            return agency;
        }
        public Agency? GetByName(string name)
        {
            return _context.Set<Agency>().FirstOrDefault(agency => agency.Name == name);

        }

        public IEnumerable<Agency> GetAll()
        {
            return _context.Set<Agency>().ToList();
        }
    }
}

   
