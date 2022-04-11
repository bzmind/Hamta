using MediatR;

namespace Common.Application.Base_Classes;

public interface IBaseCommandHandler<TRequest> : IRequestHandler<TRequest, OperationResult>
    where TRequest : IBaseCommand
{
    
}