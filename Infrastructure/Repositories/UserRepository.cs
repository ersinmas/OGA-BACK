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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public Task<User> GetByEmail(User user)
        {
           var usuario =  _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
           return usuario;
        }
    }
}
