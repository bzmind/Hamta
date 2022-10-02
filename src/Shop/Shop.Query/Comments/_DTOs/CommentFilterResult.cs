using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.CommentAggregate;

namespace Shop.Query.Comments._DTOs;

public class CommentFilterResult : BaseFilterResult<CommentDto, CommentFilterParams>
{
}

public class CommentFilterParams : BaseFilterParams
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public Comment.CommentStatus? Status { get; set; }
}

public class ProductCommentFilterParams : BaseFilterParams
{
    public long? ProductId { get; set; }
}