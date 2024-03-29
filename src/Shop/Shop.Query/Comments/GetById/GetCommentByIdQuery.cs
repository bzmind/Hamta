﻿using Common.Query.BaseClasses;
using Dapper;
using Shop.Infrastructure;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Comments.GetById;

public record GetCommentByIdQuery(long CommentId) : IBaseQuery<CommentDto?>;

public class GetCommentByIdQueryHandler : IBaseQueryHandler<GetCommentByIdQuery, CommentDto?>
{
    private readonly DapperContext _dapperContext;

    public GetCommentByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<CommentDto?> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"SELECT
                        c.*,
                        cs.FullName AS UserFullName,
                        ch.*
                    FROM {_dapperContext.Comments} c
                    INNER JOIN {_dapperContext.CommentPoints} ch
                        ON c.Id = ch.CommentId
                    INNER JOIN {_dapperContext.Users} cs
                        ON c.UserId = cs.Id
                    WHERE c.Id = @CommentId";

        var commentDtos = await connection.QueryAsync<CommentDto, CommentHintDto, CommentDto>(sql,
            (commentDto, hint) =>
            {
                commentDto.CommentHints.Add(hint);
                return commentDto;
            }, new { request.CommentId });

        var groupedByCommentDto = commentDtos.GroupBy(c => c.Id).Select(commentDto =>
        {
            var firstCommentDto = commentDto.First();
            firstCommentDto.CommentHints = commentDto.Select(c => c.CommentHints.Single()).ToList();
            return firstCommentDto;
        });

        return groupedByCommentDto.First();
    }
}