using FluentResults;
using MediatR;

namespace Pulse.Product.Application.Contracts.Messaging;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;