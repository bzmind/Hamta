using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Comments._DTOs;
using Shop.Query.Comments._Mappers;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentByFilterQuery : BaseFilterQuery<CommentFilterResult, CommentFilterParams>
{
    public GetCommentByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetCommentListQueryHandler : IBaseQueryHandler<GetCommentByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetCommentListQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CommentFilterResult> Handle(GetCommentByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterParams;

        var query = _shopContext.Comments
            .OrderByDescending(c => c.CreationDate)
            .Join(
                _shopContext.Users,
                comment => comment.UserId,
                user => user.Id,
                (comment, user) => comment.MapToCommentDto(user.FullName))
            .AsQueryable();

        if (@params.UserId != null)
            query = query.Where(c => c.UserId == @params.UserId);

        if (@params.ProductId != null)
            query = query.Where(c => c.ProductId == @params.ProductId);

        if (@params.Status != null)
            query = query.Where(c => c.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var groupedQueryResult = finalQuery.GroupBy(c => c.Id).Select(commentGroup =>
        {
            var firstItem = commentGroup.First();
            firstItem.CommentHints = commentGroup.Select(c => c.CommentHints).First();
            return firstItem;
        }).ToList();

        return new CommentFilterResult
        {
            Data = groupedQueryResult,
            FilterParam = @params
        };
    }
}