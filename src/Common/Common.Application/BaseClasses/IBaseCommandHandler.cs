using MediatR;

namespace Common.Application.BaseClasses;

public interface IBaseCommandHandler<TRequest> : IRequestHandler<TRequest, OperationResult>
    where TRequest : IBaseCommand
{
    
}