using MediatR;

namespace Common.Query.BaseClasses;

public interface IBaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IBaseQuery<TResponse>
    where TResponse : class?
{

}