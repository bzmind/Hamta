using System.Text.Json.Serialization;

namespace Common.Query.BaseClasses.FilterQuery;

public class BaseFilter
{
    [JsonInclude]
    public int EntityCount { get; private set; }

    [JsonInclude]
    public int CurrentPage { get; private set; }

    [JsonInclude]
    public int PageCount { get; private set; }

    [JsonInclude]
    public int StartPage { get; private set; }

    [JsonInclude]
    public int EndPage { get; private set; }

    [JsonInclude]
    public int Take { get; private set; }
    
    public void GeneratePaging(int dataCount, int take, int currentPage)
    {
        var entityCount = dataCount;
        var pageCount = (int)Math.Ceiling(entityCount / (double)take);
        PageCount = pageCount;
        CurrentPage = currentPage;
        EndPage = (currentPage + 5 > pageCount) ? pageCount : currentPage + 5;
        EntityCount = entityCount;
        Take = take;
        StartPage = (currentPage - 4 <= 0) ? 1 : currentPage - 4;
    }
}

public class BaseFilterResult<TData, TParam> : BaseFilter
    where TParam : BaseFilterParams
    where TData : BaseDto
{
    public List<TData> Data { get; set; } = new();
    public TParam FilterParams { get; set; }
}