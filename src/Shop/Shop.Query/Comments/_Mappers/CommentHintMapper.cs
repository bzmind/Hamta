using Shop.Domain.CommentAggregate;
using Shop.Query.Comments._DTOs;

namespace Shop.Query.Comments._Mappers;

internal static class CommentHintMapper
{
    public static CommentHintDto MapToHintDto(this CommentHint? hint)
    {
        if (hint == null)
            return null;

        return new CommentHintDto
        {
            Id = hint.Id,
            CreationDate = hint.CreationDate,
            CommentId = hint.CommentId,
            Status = hint.Status,
            Hint = hint.Hint
        };
    }

    public static List<CommentHintDto> MapToHintDto(this List<CommentHint> hints)
    {
        var dtoHints = new List<CommentHintDto>();

        hints.ForEach(hint =>
        {
            dtoHints.Add(new CommentHintDto
            {
                Id = hint.Id,
                CreationDate = hint.CreationDate,
                CommentId = hint.CommentId,
                Status = hint.Status,
                Hint = hint.Hint
            });
        });

        return dtoHints;
    }
}