using MediatR;

namespace Common.Application.Base_Classes;

public interface IBaseCommand : IRequest<OperationResult>
{
    
}