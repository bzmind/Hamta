using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Questions._DTOs;
using Shop.Query.Questions._Mappers;

namespace Shop.Query.Questions.GetByFilter;

public class GetQuestionByFilterQuery : BaseFilterQuery<QuestionFilterResult, QuestionFilterParams>
{
    public GetQuestionByFilterQuery(QuestionFilterParams filterParams) : base(filterParams)
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
        var @params = request.FilterFilterParams;

        var query = _shopContext.Questions
            .OrderByDescending(q => q.CreationDate)
            .Join(
                _shopContext.Users,
                q => q.UserId,
                c => c.Id,
                (question, user) => question.MapToQuestionDto(user))
            .AsQueryable();

        if (@params.ProductId != null)
            query = query.Where(q => q.ProductId == @params.ProductId);

        if (@params.UserId != null)
            query = query.Where(q => q.UserId == @params.UserId);

        if (@params.Status != null)
            query = query.Where(q => q.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var repliesUserIds = new List<long>();
        queryResult.ForEach(qDto =>
        {
            qDto.Replies.ForEach(rDto =>
            {
                repliesUserIds.Add(rDto.UserId);
            });
        });

        var users = await _shopContext.Users
            .Where(c => repliesUserIds.Contains(c.Id)).ToListAsync(cancellationToken);

        queryResult.ForEach(qDto =>
        {
            qDto.Replies.ForEach(rDto =>
            {
                var user = users.First(c => c.Id == rDto.UserId);
                rDto.UserFullName = user.FullName;
            });
        });

        var model = new QuestionFilterResult
        {
            Data = queryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}