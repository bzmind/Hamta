using Common.Api;
using Shop.Query.Questions._DTOs;
using Shop.UI.Models.Questions;

namespace Shop.UI.Services.Questions;

public interface IQuestionService
{
    Task<ApiResult?> Create(CreateQuestionViewModel model);
    Task<ApiResult?> SetStatus(SetQuestionStatusCommandViewModel model);
    Task<ApiResult?> AddReply(AddReplyViewModel model);
    Task<ApiResult?> RemoveReply(RemoveReplyCommandViewModel model);
    Task<ApiResult?> Remove(long questionId);

    Task<QuestionDto?> GetById(long questionId);
    Task<QuestionFilterResult?> GetByFilter(QuestionFilterParamsViewModel filterParams);
}