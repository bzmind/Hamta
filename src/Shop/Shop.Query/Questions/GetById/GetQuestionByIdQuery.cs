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
        var tables = await _shopContext.Questions
            .Where(q => q.Id == request.QuestionId)
            .Join(
                _shopContext.Customers,
                q => q.CustomerId,
                c => c.Id,
                (question, customer) => new
                {
                    question,
                    customer
                })
            .FirstOrDefaultAsync(cancellationToken);

        var repliesCustomerIds = new List<long>();
        tables?.question.Replies.ToList().ForEach(rDto =>
        {
            repliesCustomerIds.Add(rDto.CustomerId);
        });

        var customers = await _shopContext.Customers
            .Where(c => repliesCustomerIds.Contains(c.Id)).ToListAsync(cancellationToken);

        var questionDto = tables.question.MapToQuestionDto(tables.customer.FullName);

        questionDto.Replies.ForEach(rDto =>
        {
            var customer = customers.First(c => c.Id == rDto.CustomerId);
            rDto.CustomerFullName = customer.FullName;
        });

        return questionDto;
    }
}