using Common.Application;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.SetStatus;
using Shop.Query.Questions._DTOs;

namespace Shop.Presentation.Facade.Questions;

public interface IQuestionFacade
{
    Task<OperationResult> Create(CreateQuestionCommand command);
    Task<OperationResult> SetStatus(SetQuestionStatusCommand command);

    Task<QuestionDto?> GetQuestionById(long id);
    Task<QuestionFilterResult> GetQuestionByFilter(QuestionFilterParam filterParams);
}