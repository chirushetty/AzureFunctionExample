using Domain;
using Domain.States;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateUserCommandHandler : ICreateUserCommandHandler
    {
        private IUserRepository _userRepository;
        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public Task<OneOf<UserCreated, UserAlreadyExist>> HandleAsync(CreateUserCommand request)
        {
            throw new NotImplementedException();
        }
    }
}
