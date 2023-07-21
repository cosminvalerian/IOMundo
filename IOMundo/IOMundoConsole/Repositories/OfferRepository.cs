using IOMundoConsole.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOMundoConsole.Repositories.Interfaces
{
    public class OfferRepository : BaseRepository<Offer>, IOfferRepository
    {
        public OfferRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Offer>> GetAvailable(DateTime checkInDate, uint duration, string combination)
        {
            return await _dbSet.Where(o => o.CheckInDate == checkInDate && o.StayDurationNights == duration && o.PersonCombination == combination).ToListAsync();
        }
    }
}
