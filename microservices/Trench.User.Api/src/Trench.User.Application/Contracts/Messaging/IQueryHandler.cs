using FluentResults;
using MediatR;

namespace Trench.User.Application.Contracts.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;