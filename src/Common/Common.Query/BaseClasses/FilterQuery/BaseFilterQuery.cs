namespace Common.Query.BaseClasses.FilterQuery;

public class BaseFilterQuery<TResponse, TParam> : IBaseQuery<TResponse> where TResponse : BaseFilter
    where TParam : BaseFilterParam
{
    public TParam FilterParams { get; set; }

    public BaseFilterQuery(TParam filterParams)
    {
        FilterParams = filterParams;
    }
}