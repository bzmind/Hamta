using Common.Application;
using Shop.Application.Questions.AddReply;
using Shop.Application.Questions.Create;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;
using Shop.Query.Questions._DTOs;

namespace Shop.Presentation.Facade.Questions;

public interface IQuestionFacade
{
    Task<OperationResult<long>> Create(CreateQuestionCommand command);
    Task<OperationResult> AddReply(AddReplyCommand command);
    Task<OperationResult> RemoveReply(RemoveReplyCommand command);
    Task<OperationResult> SetStatus(SetQuestionStatusCommand command);
    Task<OperationResult> Remove(long questionId);

    Task<QuestionDto?> GetById(long id);
    Task<QuestionFilterResult> GetByFilter(QuestionFilterParams filterParams);
}