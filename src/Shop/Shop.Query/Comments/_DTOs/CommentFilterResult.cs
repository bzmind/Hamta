using Common.Query.BaseClasses.FilterQuery;

namespace Shop.Query.Comments._DTOs;

public class CommentFilterResult : BaseFilterResult<CommentDto, CommentFilterParams>
{

}

public class CommentFilterParams : BaseFilterParam
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public string? Status { get; set; }
}