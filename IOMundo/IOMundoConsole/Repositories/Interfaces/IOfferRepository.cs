using IOMundoConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoConsole.Repositories.Interfaces
{
    public interface IOfferRepository : IBaseRepository<Offer>
    {
        Task<List<Offer>> GetAvailable(DateTime checkInDate, uint duration, string combination);
    }
}
