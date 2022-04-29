using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions._Mappers;

namespace Shop.Query.Questions.GetById;

public record GetQuestionByIdQuery(long QuestionId) : IBaseQuery<QuestionDto>;

public class GetQuestionByIdQueryHandler : IBaseQueryHandler<GetQuestionByIdQuery, QuestionDto>
{
    private readonly ShopContext _shopContext;

    public GetQuestionByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<QuestionDto> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var question = await _shopContext.Questions
            .FirstOrDefaultAsync(q => q.Id == request.QuestionId, cancellationToken);

        return question.MapToQuestionDto();
    }
}