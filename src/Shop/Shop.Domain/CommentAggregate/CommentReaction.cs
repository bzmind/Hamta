using Common.Domain.BaseClasses;

namespace Shop.Domain.CommentAggregate;

public class CommentReaction : BaseEntity
{
    public long CommentId { get; set; }
    public long CustomerId { get; set; }
    public string Reaction { get; set; }

    public enum ReactionType
    {
        Like,
        Dislike
    }

    private CommentReaction()
    {
        
    }

    public CommentReaction(long commentId, long customerId, ReactionType reactionType)
    {
        CommentId = commentId;
        CustomerId = customerId;
        Reaction = reactionType.ToString();
    }
}