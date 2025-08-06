using FluentResults;
using MediatR;

namespace Trench.User.Application.Contracts.Messaging;

public interface IQuery<TRequest> : IRequest<Result<TRequest>>;