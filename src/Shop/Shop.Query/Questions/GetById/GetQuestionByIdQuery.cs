using Common.Query.BaseClasses;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions._Mappers;

namespace Shop.Query.Questions.GetById;

public record GetQuestionByIdQuery(long QuestionId) : IBaseQuery<QuestionDto?>;

public class GetQuestionByIdQueryHandler : IBaseQueryHandler<GetQuestionByIdQuery, QuestionDto?>
{
    private readonly ShopContext _shopContext;

    public GetQuestionByIdQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<QuestionDto?> Handle(GetQuestionByIdQuery request, CancellationToken cancellationToken)
    {
        var questionDto = await _shopContext.Questions
            .Where(q => q.Id == request.QuestionId)
            .Join(
                _shopContext.Customers,
                q => q.CustomerId,
                c => c.Id,
                (question, customer) => question.MapToQuestionDto(customer.FullName))
            .FirstOrDefaultAsync(cancellationToken);

        return questionDto;
    }
}