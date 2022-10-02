using Shop.Domain.AvatarAggregate;
using Shop.Domain.CommentAggregate;
using Shop.Domain.UserAggregate;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Comments._Mappers;

internal static class CommentMapper
{ 
    public static CommentDto MapToCommentDto(this Comment? comment, User user, Avatar avatar)
    {
        if (comment == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            UserId = comment.UserId,
            ProductId = comment.ProductId,
            UserFullName = user.FullName,
            UserAvatar = avatar.Name,
            Title = comment.Title,
            Description = comment.Description,
            CommentHints = comment.CommentPoints.ToList().MapToHintDto(),
            Status = comment.Status,
            Recommendation = comment.Recommendation,
            Likes = comment.Likes,
            Dislikes = comment.Dislikes
        };
    }
}