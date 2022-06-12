using Shop.Domain.QuestionAggregate;
using Shop.UI.Models._Filters;

namespace Shop.UI.Models.Questions;

public class QuestionFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? ProductId { get; set; }
    public long? UserId { get; set; }
    public Question.QuestionStatus? Status { get; set; }
}