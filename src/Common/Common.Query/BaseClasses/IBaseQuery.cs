using MediatR;

namespace Common.Query.BaseClasses;

public interface IBaseQuery<TResponse> : IRequest<TResponse> where TResponse : class
{
    
}