using Trench.User.Application.Contracts.Messaging;

namespace Trench.User.Application.UseCases.User.Commands.RegisterUser;

public sealed record RegisterUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    DateTime BirthDate,
    string Password) 
    : ICommand<bool>;