using Shop.Domain.QuestionAggregate;

namespace Shop.UI.Models.Questions;

public class QuestionFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? ProductId { get; set; }
    public long? UserId { get; set; }
    public Question.QuestionStatus? Status { get; set; }
}