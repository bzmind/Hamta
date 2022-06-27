using MediatR;

namespace Common.Application.BaseClasses;

public interface IBaseCommand : IRequest<OperationResult>
{

}

public interface IBaseCommand<TResponse> : IRequest<OperationResult<TResponse>>
{

}