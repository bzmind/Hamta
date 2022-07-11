using Shop.Domain.CommentAggregate;

namespace Shop.API.ViewModels.Comments;

public class SetCommentStatusViewModel
{
    public long CommentId { get; set; }
    public Comment.CommentStatus Status { get; set; }
}