using Shop.Domain.CommentAggregate;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Comments._Mappers;

internal static class CommentMapper
{ 
    public static CommentDto MapToCommentDto(this Comment? comment, string userFullName)
    {
        if (comment == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            UserId = comment.UserId,
            ProductId = comment.ProductId,
            UserFullName = userFullName,
            Title = comment.Title,
            Description = comment.Description,
            CommentHints = comment.CommentHints.ToList().MapToHintDto(),
            Status = comment.Status,
            Recommendation = comment.Recommendation,
            Likes = comment.Likes,
            Dislikes = comment.Dislikes
        };
    }
}