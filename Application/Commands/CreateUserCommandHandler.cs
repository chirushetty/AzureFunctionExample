using Domain;
using Domain.States;
using OneOf;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class CreateUserCommandHandler : ICreateUserCommandHandler
    {
        private IUserRepository _userRepository;
        public CreateUserCommandHandler( IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<OneOf<UserCreated, UserAlreadyExist>> HandleAsync(CreateUserCommand request)
        {

            var user = User.Create(request.UserName, request.FistName, request.LastName, request.Email);
            var status = await _userRepository.CheckIfUserExist(user);
            if (status) _ = _userRepository.AddUserAsync(user);
            else return new UserAlreadyExist();
            return new UserCreated();
        }
    }
}
