using Common.Query.BaseClasses;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class QuestionDto : BaseDto
{
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string UserFullName { get; set; }
    public string Description { get; set; }
    public Question.QuestionStatus Status { get; set; }
    public List<ReplyDto> Replies { get; set; }
}