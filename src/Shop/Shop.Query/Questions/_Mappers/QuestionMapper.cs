using Shop.Domain.QuestionAggregate;
using Shop.Domain.UserAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class QuestionMapper
{
    public static QuestionDto MapToQuestionDto(this Question? question, User user)
    {
        if (question == null)
            return null;

        var questionDto = new QuestionDto
        {
            Id = question.Id,
            CreationDate = question.CreationDate,
            ProductId = question.ProductId,
            UserId = question.UserId,
            UserFullName = user.FullName,
            Description = question.Description,
            Replies = new List<ReplyDto>(),
            Status = question.Status
        };

        question.Replies.ToList().ForEach(r => questionDto.Replies.Add(new ReplyDto
        {
            Id = r.Id,
            CreationDate = r.CreationDate,
            QuestionId = r.QuestionId,
            ProductId = r.ProductId,
            UserId = r.UserId,
            UserFullName = null,
            Description = r.Description,
            Status = r.Status
        }));

        return questionDto;
    }
}