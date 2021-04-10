using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IUserRepository
    {
        Task<bool> CheckIfUserExist(User user);
        Task AddUserAsync(User user);
    }
}
