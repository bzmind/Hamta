using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class QuestionFilterResult : BaseFilterResult<QuestionDto, QuestionFilterParams>
{

}

public class QuestionFilterParams : BaseFilterParams
{
    public long? ProductId { get; set; }
    public long? UserId { get; set; }
    public Question.QuestionStatus? Status { get; set; }
}