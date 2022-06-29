using Shop.Domain.QuestionAggregate;

namespace Shop.UI.Models.Questions;

public class SetQuestionStatusViewModel
{
    public long QuestionId { get; set; }
    public Question.QuestionStatus Status { get; set; }
}