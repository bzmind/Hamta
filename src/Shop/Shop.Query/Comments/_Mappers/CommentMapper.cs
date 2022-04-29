using Shop.Domain.CommentAggregate;
using Shop.Domain.CustomerAggregate;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Comments._Mappers;

internal static class CommentMapper
{ 
    public static CommentDto MapToCommentDto(this Comment? comment, Customer? customer)
    {
        if (comment == null || customer == null)
            return null;

        return new CommentDto
        {
            Id = comment.Id,
            CreationDate = comment.CreationDate,
            CustomerId = comment.CustomerId,
            ProductId = comment.ProductId,
            CustomerFullName = customer.FullName,
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