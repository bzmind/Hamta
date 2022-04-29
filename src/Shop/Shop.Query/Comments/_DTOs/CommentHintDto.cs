using Common.Query.BaseClasses;
using Shop.Domain.CommentAggregate;

namespace Shop.Query.Comments._DTOs;

public class CommentHintDto : BaseDto
{
    public long CommentId { get; set; }
    public CommentHint.HintStatus Status { get; set; }
    public string Hint { get; set; }
}