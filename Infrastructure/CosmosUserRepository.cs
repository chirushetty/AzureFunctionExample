using Domain;
using Domain.States;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CosmosUserRepository : IUserRepository
    {
        private DatabaseContext _context { get; set; }
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

        public Task<bool> CheckIfUserExist(User user)
        {
            throw new NotImplementedException();
        }
    }
}
