using Common.Query.BaseClasses;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class ReplyDto : BaseDto
{
    public long ParentId { get; set; }
    public string Description { get; set; }
    public Reply.ReplyStatus Status { get; set; }
}