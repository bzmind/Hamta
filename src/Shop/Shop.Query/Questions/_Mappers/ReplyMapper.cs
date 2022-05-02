using Shop.Domain.QuestionAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class ReplyMapper
{
    public static ReplyDto MapToReplyDto(this Reply? reply)
    {
        if (reply == null)
            return null;

        return new ReplyDto
        {
            Id = reply.Id,
            CreationDate = reply.CreationDate,
            ParentId = reply.ParentId,
            Description = reply.Description,
            Status = reply.Status
        };
    }

    public static List<ReplyDto> MapToReplyDto(this List<Reply> replies)
    {
        var dtoReplies = new List<ReplyDto>();

        replies.ForEach(reply =>
        {
            dtoReplies.Add(new ReplyDto
            {
                Id = reply.Id,
                CreationDate = reply.CreationDate,
                Description = reply.Description,
                Status = reply.Status
            });
        });

        return dtoReplies;
    }
}