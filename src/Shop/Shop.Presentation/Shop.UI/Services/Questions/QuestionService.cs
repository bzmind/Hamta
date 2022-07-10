using Common.Api;
using Shop.Query.Questions._DTOs;
using System.Text.Json;
using Shop.API.CommandViewModels.Questions;
using Shop.Application.Questions.RemoveReply;
using Shop.Application.Questions.SetStatus;

namespace Shop.UI.Services.Questions;

public class QuestionService : BaseService, IQuestionService
{
    protected override string ApiEndpointName { get; set; } = "Question";

    public QuestionService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult> Create(CreateQuestionCommandViewModel model)
    {
        return await PostAsJsonAsync("Create", model);
    }

    public async Task<ApiResult> SetStatus(SetQuestionStatusCommand model)
    {
        return await PutAsJsonAsync("SetStatus", model);
    }

    public async Task<ApiResult> AddReply(AddReplyCommandViewModel model)
    {
        return await PutAsJsonAsync("AddReply", model);
    }

    public async Task<ApiResult> RemoveReply(RemoveReplyCommand model)
    {
        return await PutAsJsonAsync("RemoveReply", model);
    }

    public async Task<ApiResult> Remove(long questionId)
    {
        return await DeleteAsync($"Remove/{questionId}");
    }

    public async Task<QuestionDto> GetById(long questionId)
    {
        var result = await GetFromJsonAsync<QuestionDto>($"GetById/{questionId}");
        return result.Data;
    }

    public async Task<QuestionFilterResult> GetByFilter(QuestionFilterParams filterParams)
    {
        var url = $"api/question/GetByFilterPageId={filterParams.PageId}&Take={filterParams.Take}" +
                  $"&ProductId={filterParams.ProductId}&UserId={filterParams.UserId}&Status={filterParams.Status}";
        var result = await GetFromJsonAsync<QuestionFilterResult>(url);
        return result.Data;
    }
}