using MediatR;

namespace Common.Application.BaseClasses;

public interface IBaseCommandHandler<TCommand> : IRequestHandler<TCommand, OperationResult>
    where TCommand : IBaseCommand
{

}

public interface IBaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, OperationResult<TResponse>>
    where TCommand : IBaseCommand<TResponse>
{

}