using Common.Query.BaseClasses.FilterQuery;
using Shop.Domain.QuestionAggregate;

namespace Shop.Query.Questions._DTOs;

public class QuestionFilterResult : BaseFilterResult<QuestionDto, QuestionFilterParam>
{
    
}

public class QuestionFilterParam : BaseFilterParam
{
    public long? ProductId { get; set; }
    public long? CustomerId { get; set; }
    public Question.QuestionStatus? Status { get; set; }
}