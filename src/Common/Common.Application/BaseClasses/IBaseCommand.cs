using MediatR;

namespace Common.Application.BaseClasses;

public interface IBaseCommand : IRequest<OperationResult>
{
    
}