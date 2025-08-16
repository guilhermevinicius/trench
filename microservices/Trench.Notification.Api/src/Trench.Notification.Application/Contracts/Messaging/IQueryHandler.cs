using FluentResults;
using MediatR;

namespace Trench.Notification.Application.Contracts.Messaging;

public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;