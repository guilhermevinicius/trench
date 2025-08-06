using FluentResults;
using Trench.User.Application.Contracts.Messaging;
using Trench.User.Application.Contracts.Repositories;
using Trench.User.Domain.Integrations;
using Trench.User.Domain.Resources;
using Trench.User.Domain.SeedWorks;
using Entity = Trench.User.Domain.Aggregates.Users.Entities;

namespace Trench.User.Application.UseCases.User.Commands.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IIntegrationIdentity integrationIdentity)
    : ICommandHandler<RegisterUserCommand, bool>
{
    public async Task<Result<bool>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await userRepository.AlreadyUsernameExists(command.Username, cancellationToken))
            return Result.Fail(DomainValidationResource.AlreadyUsernameExists);

        if (await userRepository.AlreadyEmailExists(command.Email, cancellationToken))
            return Result.Fail(DomainValidationResource.AlreadyEmailExists);

        var user = Entity.User.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Username,
            command.BirthDate);

        var identityId = await integrationIdentity.RegisterAsync(user, command.Password, cancellationToken);
        if (identityId is null)
            return Result.Fail(DomainValidationResource.ErrorCreatingAccount);

        user.SetIdentityId(identityId);

        await userRepository.InsertAsync(user, cancellationToken);

        return await unitOfWork.CommitAsync(cancellationToken);
    }
}