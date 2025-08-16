using FluentResults;
using MediatR;

namespace Trench.Notification.Application.Contracts.Messaging;

public interface IQuery<TRequest> : IRequest<Result<TRequest>>;