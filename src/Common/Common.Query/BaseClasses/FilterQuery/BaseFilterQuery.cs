namespace Common.Query.BaseClasses.FilterQuery;

public class BaseFilterQuery<TResponse, TParam> : IBaseQuery<TResponse> where TResponse : BaseFilter
    where TParam : BaseFilterParams
{
    public TParam FilterFilterParams { get; set; }

    public BaseFilterQuery(TParam filterFilterParams)
    {
        FilterFilterParams = filterFilterParams;
    }
}