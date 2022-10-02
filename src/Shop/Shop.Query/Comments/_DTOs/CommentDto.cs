using Common.Query.BaseClasses;
using Shop.Domain.CommentAggregate;

namespace Shop.Query.Comments._DTOs;

public class CommentDto : BaseDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public string UserFullName { get; set; }
    public string UserAvatar { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CommentHintDto> CommentHints { get; set; } = new();
    public Comment.CommentStatus Status { get; set; }
    public Comment.CommentRecommendation Recommendation { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}