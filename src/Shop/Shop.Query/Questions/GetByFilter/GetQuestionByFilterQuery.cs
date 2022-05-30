using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions._Mappers;

namespace Shop.Query.Questions.GetByFilter;

public class GetQuestionByFilterQuery : BaseFilterQuery<QuestionFilterResult, QuestionFilterParam>
{
    public GetQuestionByFilterQuery(QuestionFilterParam filterParams) : base(filterParams)
    {
    }
}

public class GetQuestionByFilterQueryHandler : IBaseQueryHandler<GetQuestionByFilterQuery, QuestionFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetQuestionByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<QuestionFilterResult> Handle(GetQuestionByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Questions
            .OrderByDescending(q => q.CreationDate)
            .Join(
                _shopContext.Customers,
                q => q.CustomerId,
                c => c.Id,
                (question, customer) => question.MapToQuestionDto(customer.FullName))
            .AsQueryable();

        if (@params.ProductId != null)
            query = query.Where(q => q.ProductId == @params.ProductId);

        if (@params.CustomerId != null)
            query = query.Where(q => q.CustomerId == @params.CustomerId);

        if (@params.Status != null)
            query = query.Where(q => q.Status == @params.Status.ToString());

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var repliesCustomerIds = new List<long>();
        finalQuery.ForEach(qDto =>
        {
            qDto.Replies.ForEach(rDto =>
            {
                repliesCustomerIds.Add(rDto.CustomerId);
            });
        });

        var customers = await _shopContext.Customers
            .Where(c => repliesCustomerIds.Contains(c.Id)).ToListAsync(cancellationToken);

        finalQuery.ForEach(qDto =>
        {
            qDto.Replies.ForEach(rDto =>
            {
                var customer = customers.First(c => c.Id == rDto.CustomerId);
                rDto.CustomerFullName = customer.FullName;
            });
        });

        return new QuestionFilterResult
        {
            Data = finalQuery,
            FilterParam = @params
        };
    }
}