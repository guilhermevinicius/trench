using FluentResults;
using MediatR;

namespace Pulse.Product.Application.Contracts.Messaging;

public interface IQuery<TRequest> : IRequest<Result<TRequest>>;