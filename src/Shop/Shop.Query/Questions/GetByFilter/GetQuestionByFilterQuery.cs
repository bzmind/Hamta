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

        var query = _shopContext.Questions.OrderByDescending(q => q.CreationDate).AsQueryable();

        if (@params.ProductId != null)
            query = query.Where(q => q.ProductId == @params.ProductId);

        if (@params.CustomerId != null)
            query = query.Where(q => q.CustomerId == @params.CustomerId);

        if (@params.Status != null)
            query = query.Where(q => q.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        return new QuestionFilterResult()
        {
            Data = await query
                .Skip(skip)
                .Take(@params.Take)
                .Select(q => q.MapToQuestionDto())
                .ToListAsync(cancellationToken),

            FilterParam = @params
        };
    }
}