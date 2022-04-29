using Shop.Domain.QuestionAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class AnswerMapper
{
    public static AnswerDto MapToAnswerDto(this Answer? answer)
    {
        if (answer == null)
            return null;

        return new AnswerDto
        {
            Id = answer.Id,
            CreationDate = answer.CreationDate,
            ParentId = answer.ParentId,
            Description = answer.Description,
            Status = answer.Status
        };
    }

    public static List<AnswerDto> MapToAnswerDto(this List<Answer> answers)
    {
        var dtoAnswers = new List<AnswerDto>();

        answers.ForEach(answer =>
        {
            dtoAnswers.Add(new AnswerDto
            {
                Id = answer.Id,
                CreationDate = answer.CreationDate,
                Description = answer.Description,
                Status = answer.Status
            });
        });

        return dtoAnswers;
    }
}