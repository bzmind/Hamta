using Common.Query.BaseClasses;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class AnswerDto : BaseDto
{
    public long ParentId { get; set; }
    public string Description { get; set; }
    public Answer.AnswerStatus Status { get; set; }
}