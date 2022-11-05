using Common.Query.BaseClasses;
using Common.Query.BaseClasses.FilterQuery;
using Microsoft.EntityFrameworkCore;
using Shop.Infrastructure.Persistence.EF;
using Shop.Query.Comments._DTOs;
using Shop.Query.Comments._Mappers;

namespace Shop.Query.Comments.GetByFilter;

public class GetCommentsByFilterQuery : BaseFilterQuery<CommentFilterResult, CommentFilterParams>
{
    public GetCommentsByFilterQuery(CommentFilterParams filterParams) : base(filterParams)
    {
    }
}

public class GetCommentsByFilterQueryHandler : IBaseQueryHandler<GetCommentsByFilterQuery, CommentFilterResult>
{
    private readonly ShopContext _shopContext;

    public GetCommentsByFilterQueryHandler(ShopContext shopContext)
    {
        _shopContext = shopContext;
    }

    public async Task<CommentFilterResult> Handle(GetCommentsByFilterQuery request, CancellationToken cancellationToken)
    {
        var @params = request.FilterFilterParams;

        var query = _shopContext.Comments
            .OrderByDescending(t => t.CreationDate)
            .Join(
                _shopContext.Users,
                comment => comment.UserId,
                user => user.Id,
                (comment, user) => new { comment, user })
            .Join(
                _shopContext.Avatars,
                tables => tables.user.AvatarId,
                avatar => avatar.Id,
                (tables, avatar) => new { tables.comment, tables.user, avatar })
            .Join(
                _shopContext.Products,
                tables => tables.comment.ProductId,
                product => product.Id,
                (tables, product) => new { tables.comment, tables.user, tables.avatar, product })
            .AsQueryable();

        if (@params.UserId != null)
            query = query.Where(t => t.comment.UserId == @params.UserId);

        if (@params.ProductId != null)
            query = query.Where(t => t.comment.ProductId == @params.ProductId);

        if (@params.Status != null)
            query = query.Where(t => t.comment.Status == @params.Status);

        var skip = (@params.PageId - 1) * @params.Take;

        var queryResult = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var dtoComments = new List<CommentDto>();

        queryResult.ForEach(tables =>
        {
            dtoComments.Add(tables.comment.MapToCommentDto(tables.user, tables.avatar, tables.product));
        });

        var groupedQueryResult = dtoComments.GroupBy(t => t.Id).Select(commentGroup =>
        {
            var firstItem = commentGroup.First();
            firstItem.CommentHints = commentGroup.Select(t => t.CommentHints).First();
            return firstItem;
        }).ToList();

        var model = new CommentFilterResult
        {
            Data = groupedQueryResult,
            FilterParams = @params
        };
        model.GeneratePaging(query.Count(), @params.Take, @params.PageId);
        return model;
    }
}