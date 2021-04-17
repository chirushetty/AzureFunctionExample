using Domain;
using Domain.States;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CosmosUserRepository : IUserRepository
    {
        private DatabaseContext _context;
        public CosmosUserRepository(DatabaseContext context)
        {
            _context = context;
        }
        public async Task AddUserAsync(User user)
        {
            //var user = _context.Users.Find
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
            //return new UserCreated();
        }

        public async Task<bool> CheckIfUserExist(GenericSpecification<User> query)
        {
            var user = await _context.Users.Where(query.Expression).ToListAsync();

           return user == null ? true : false;
        }
    }
}
