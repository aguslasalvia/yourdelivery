using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAgencyRepository
    {
        Agency Add ( Agency Agency );
        Agency Update ( Agency Agency );
        Agency Delete ( Agency Agency );
        Agency GetById ( int id );

        IEnumerable<Agency> GetAll ();
    }
}
