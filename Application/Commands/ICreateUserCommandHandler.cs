using Application.Handlers;
using Domain.States;
using OneOf;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ICreateUserCommandHandler : IHandler<CreateUserCommand, OneOf<UserCreated, UserAlreadyExist>>
    {
    }
}
