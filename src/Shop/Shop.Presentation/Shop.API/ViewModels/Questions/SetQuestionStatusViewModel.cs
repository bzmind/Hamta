using Shop.Domain.QuestionAggregate;

namespace Shop.API.ViewModels.Questions;

public class SetQuestionStatusViewModel
{
    public long QuestionId { get; set; }
    public Question.QuestionStatus Status { get; set; }
}