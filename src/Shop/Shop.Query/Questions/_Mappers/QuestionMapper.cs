using Shop.Domain.QuestionAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class QuestionMapper
{
    public static QuestionDto MapToQuestionDto(this Question? question, string customerFullName)
    {
        if (question == null)
            return null;

        var questionDto = new QuestionDto
        {
            Id = question.Id,
            CreationDate = question.CreationDate,
            ProductId = question.ProductId,
            CustomerId = question.CustomerId,
            CustomerFullName = customerFullName,
            Description = question.Description,
            Replies = new List<ReplyDto>(),
            Status = question.Status
        };

        question.Replies.ToList().ForEach(r => questionDto.Replies.Add(new ReplyDto()
        {
            Id = r.Id,
            CreationDate = r.CreationDate,
            QuestionId = r.QuestionId,
            ProductId = r.ProductId,
            CustomerId = r.CustomerId,
            CustomerFullName = null,
            Description = r.Description,
            Status = r.Status
        }));

        return questionDto;
    }
}