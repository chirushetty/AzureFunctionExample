using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserRepository
    {
        Task<bool> CheckIfUserExist(GenericSpecification<User> query);
        Task AddUserAsync(User user);
    }
}
