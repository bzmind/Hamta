using Shop.Domain.QuestionAggregate;
using Shop.Query.Questions._DTOs;

namespace Shop.Query.Questions._Mappers;

internal static class QuestionMapper
{
    public static QuestionDto MapToQuestionDto(this Question? question)
    {
        if (question == null)
            return null;

        return new QuestionDto
        {
            Id = question.Id,
            CreationDate = question.CreationDate,
            ProductId = question.ProductId,
            CustomerId = question.CustomerId,
            Description = question.Description,
            Answers = question.Answers.ToList().MapToAnswerDto(),
            Status = question.Status
        };
    }

    public static List<QuestionDto> MapToQuestionDto(this List<Question> questions)
    {
        var dtoQuestions = new List<QuestionDto>();

        questions.ForEach(question =>
        {
            dtoQuestions.Add(new QuestionDto
            {
                Id = question.Id,
                CreationDate = question.CreationDate,
                ProductId = question.ProductId,
                CustomerId = question.CustomerId,
                Description = question.Description,
                Answers = question.Answers.ToList().MapToAnswerDto(),
                Status = question.Status
            });
        });

        return dtoQuestions;
    }
}