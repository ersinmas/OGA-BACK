using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TrailerRepository : GenericRepository<Trailer>, ITrailerRepository
    {
        public TrailerRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Trailer>> GetTrailersByMaxWeightAsync(double maxWeight)
        {
            return await _dbSet.Where(t => t.MaxWeight <= maxWeight).ToListAsync();
        }
    }
}
