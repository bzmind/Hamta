﻿using Common.Api;
using Shop.API.ViewModels.Questions;
using Shop.Query.Questions._DTOs;

namespace Shop.UI.Services.Questions;

public interface IQuestionService
{
    Task<ApiResult> Create(CreateQuestionViewModel model);
    Task<ApiResult> SetStatus(SetQuestionStatusViewModel model);
    Task<ApiResult> AddReply(AddReplyViewModel model);
    Task<ApiResult> RemoveReply(RemoveReplyViewModel model);
    Task<ApiResult> Remove(long questionId);

    Task<QuestionDto?> GetById(long questionId);
    Task<QuestionFilterResult> GetByFilter(QuestionFilterParams filterParams);
}