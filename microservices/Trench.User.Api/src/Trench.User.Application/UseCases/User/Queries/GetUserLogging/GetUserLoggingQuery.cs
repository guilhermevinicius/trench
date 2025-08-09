using Trench.User.Application.Contracts.Messaging;
using Dtos = Trench.User.Domain.Aggregates.Users.Dtos;

namespace Trench.User.Application.UseCases.User.Queries.GetUserLogging;

public sealed record GetUserLoggingQuery(
    int UserId) 
    : IQuery<Dtos.GetUserLoggingDto>;