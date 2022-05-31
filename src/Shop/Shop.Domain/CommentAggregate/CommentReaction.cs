using Common.Domain.BaseClasses;

namespace Shop.Domain.CommentAggregate;

public class CommentReaction : BaseEntity
{
    public long CommentId { get; set; }
    public long UserId { get; set; }
    public string Reaction { get; set; }

    public enum ReactionType
    {
        Like,
        Dislike
    }

    private CommentReaction()
    {
        
    }

    public CommentReaction(long commentId, long userId, ReactionType reactionType)
    {
        CommentId = commentId;
        UserId = userId;
        Reaction = reactionType.ToString();
    }
}