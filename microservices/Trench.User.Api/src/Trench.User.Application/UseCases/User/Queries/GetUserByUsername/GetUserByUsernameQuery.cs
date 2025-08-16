using Trench.User.Application.Contracts.Messaging;
using Trench.User.Domain.Aggregates.Users.Dtos;

namespace Trench.User.Application.UseCases.User.Queries.GetUserByUsername;

public sealed record GetUserByUsernameQuery(
    string IdentityId,
    string Username)
    : IQuery<GetUserLoggingDto>;