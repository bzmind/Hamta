using Common.Query.BaseClasses;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class QuestionDto : BaseDto
{
    public long ProductId { get; set; }
    public long CustomerId { get; set; }
    public string Description { get; set; }
    public List<AnswerDto> Answers { get; set; }
    public Question.QuestionStatus Status { get; set; }
}