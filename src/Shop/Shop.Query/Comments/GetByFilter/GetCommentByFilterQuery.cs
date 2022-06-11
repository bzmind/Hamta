﻿using Common.Query.BaseClasses;
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
                (comment, user) => new
                {
                    comment,
                    user
                })
            .AsQueryable();

        if (@params.UserId != null)
            query = query.Where(c => c.comment.UserId == @params.UserId);

        if (@params.ProductId != null)
            query = query.Where(c => c.comment.ProductId == @params.ProductId);

        if (@params.Status != null)
            query = query.Where(c => c.comment.Status == @params.Status.ToString());

        var skip = (@params.PageId - 1) * @params.Take;

        var finalQuery = await query
            .Skip(skip)
            .Take(@params.Take)
            .ToListAsync(cancellationToken);

        var dtoComments = new List<CommentDto>();

        finalQuery.ForEach(tables =>
        {
            dtoComments.Add(tables.comment.MapToCommentDto(tables.user.FullName));
        });

        var groupedQueryResult = dtoComments.GroupBy(c => c.Id).Select(commentGroup =>
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